using DevSkill.Core.Application.UtilitiesContracts;
using DevSkill.Core.Domain;
using DevSkill.Core.Domain.EmailServiceContracts;
using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Constants;
using DevSkill.Inventory.Domain.Templates;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Infrastructure.Utilities;
using DevSkill.Inventory.Web.Models.IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace DevSkill.Inventory.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Fields

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailUtility _emailUtility;
        private readonly ICaptchaService _captchaService;
        private readonly IUserRedirectionService _userRedirectionService;
        private readonly IEmailService _emailService;
        private readonly IAccountConfirmationEmailTemplate _accountConfirmationEmailTemplate;
        private readonly IPasswordResetEmailTemplate _passwordResetEmailTemplate;
        private readonly IPasswordChangeEmailTemplate _passwordChangeEmailTemplate;
        private readonly IServerTime _serverTime;

        #endregion

        #region Ctor
        public AccountController(UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailUtility emailUtility,
            ICaptchaService captchaService,
            IUserRedirectionService userRedirectionService,
            IEmailService emailService,
            IAccountConfirmationEmailTemplate accountConfirmationEmailTemplate,
            IPasswordResetEmailTemplate passwordResetEmailTemplate,
            IPasswordChangeEmailTemplate passwordChangeEmailTemplate,
            IServerTime serverTime)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = IdentityHelper.GetEmailStore(userManager, userStore);
            _signInManager = signInManager;
            _logger = logger;
            _emailUtility = emailUtility;
            _captchaService = captchaService;
            _userRedirectionService = userRedirectionService;
            _emailService = emailService;
            _accountConfirmationEmailTemplate = accountConfirmationEmailTemplate;
            _passwordResetEmailTemplate = passwordResetEmailTemplate;
            _passwordChangeEmailTemplate = passwordChangeEmailTemplate;
            _serverTime = serverTime;
        }
        #endregion

        #region Login Register Logout

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                TempData["success"] = "You are already logged in.";
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                returnUrl = Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var model = new LoginModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };

            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, model.ErrorMessage);
            }

            return View(model);
        }


        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                TempData["success"] = "You are already logged in.";
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var reCaptchaToken = Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(reCaptchaToken))
            {
                ModelState.AddModelError(string.Empty, "ReCaptcha validation failed. Please try again.");
            }

            var captchaResult = await _captchaService.VerifyAsync(reCaptchaToken);

            if (!captchaResult.IsValid && false)
            {
                ModelState.AddModelError(string.Empty, captchaResult.ErrorMessage);
                return View(model);
            }

            ApplicationUser? user = null;

            if (model.EmailOrUserName!.Contains('@'))
            {
                user = await _userManager.FindByEmailAsync(model.EmailOrUserName);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.EmailOrUserName);
            }
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            if (!user.EmailConfirmed)
            {
                await SendEmailConfirmationAsync(user, model.ReturnUrl);
                return RedirectToAction("RegisterConfirmation", new
                {
                    email = user.Email,
                    returnUrl = model.ReturnUrl,
                    message = "You must confirm your email address before you can log in. We've sent a new confirmation email to your address."
                });
            }

            var result = await _signInManager.PasswordSignInAsync(model.EmailOrUserName, model.Password,
                         model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                TempData["success"] = "You have successfully logged in.";

                if (string.IsNullOrEmpty(model.ReturnUrl) || model.ReturnUrl == "/" || !Url.IsLocalUrl(model.ReturnUrl))
                {
                    var redirectUrl = await _userRedirectionService.GetRedirectUrlAfterLoginAsync(User);
                    return Redirect(redirectUrl);
                }
                else
                {
                    return Redirect(model.ReturnUrl);
                }
            }

            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your account has been locked out due to multiple failed login attempts. Please try again later.");
                return RedirectToPage("./Lockout");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Your account is not allowed to sign in. Please confirm your email or contact support.");
            }
            else if (result.RequiresTwoFactor)
            {
                ModelState.AddModelError(string.Empty, "Requires two factor authentication");
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(string? returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                returnUrl = Url.Content("~/");

            var model = new RegisterModel();
            model.ReturnUrl = returnUrl;

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.DateOfBirth = DateTime.UtcNow.AddYears(-18);

            return View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl) || !Url.IsLocalUrl(model.ReturnUrl))
                model.ReturnUrl = Url.Content("~/");

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Failed to register. Please correct the errors and try again.";
                return View(model);
            }

            var reCaptchaToken = Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(reCaptchaToken))
            {
                ModelState.AddModelError(string.Empty, "ReCaptcha validation failed. Please try again.");
            }

            var captchaResult = await _captchaService.VerifyAsync(reCaptchaToken);
            if (!captchaResult.IsValid && false)
            {
                ModelState.AddModelError(string.Empty, captchaResult.ErrorMessage);
                return View(model);
            }

            var user = IdentityHelper.CreateUser();

            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                TempData["success"] = "Registration successful. Please check your email to confirm your account.";
                await _userManager.AddToRoleAsync(user, ApplicationRoles.Memeber);
                await SendEmailConfirmationAsync(user, model.ReturnUrl);

                return RedirectToAction("RegisterConfirmation", new
                {
                    email = user.Email,
                    returnUrl = model.ReturnUrl
                });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            TempData["error"] = "Failed to register. Please correct the errors and try again.";

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> RegisterConfirmation(string email,
            string? returnUrl = null, string? message = null)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Register");
            }

            var model = new RegisterConfirmationModel
            {
                Email = email,
                ReturnUrl = returnUrl,
                Message = message
            };

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, string? returnUrl = null)
        {
            var model = new ConfirmEmailModel();
            model.returnUrl ??= Url.Content("~/");

            if (userId == null || code == null)
            {
                model.ErrorMessage = "Invalid email confirmation link.";
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                model.ErrorMessage = $"Unable to load user with ID {userId}";
                return View(model);
            }

            if (user.EmailConfirmed)
            {
                model.IsSuccess = true;
                model.ErrorMessage = "Your email is already confirmed. You can now log in.";
                return View(model);
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                model.IsSuccess = true;
                return View(model);
            }
            else
            {
                model.IsSuccess = false;
                model.ErrorMessage = "The confirmation link is invalid or has expired. A new confirmation email has been sent to your email address.";
                model.email = user.Email;
                await SendEmailConfirmationAsync(user, returnUrl);

                return View(model);
            }
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> ResendConfirmationEmail(string email,
            string? returnUrl = null)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !user.EmailConfirmed)
            {
                return RedirectToAction("Login");
            }

            await SendEmailConfirmationAsync(user, returnUrl);
            return RedirectToAction("RegisterConfirmation", new { email, returnUrl });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null,
            string? remoteError = null)
        {
            returnUrl ??= Url.Content("~/");
            if (remoteError != null)
            {
                TempData["error"] = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(RegisterAsync));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
                var profilePicture = GetProfilePictureUrl(info.ProviderDisplayName, info);

                if (string.IsNullOrEmpty(email))
                    email = $"{info.ProviderKey}@{info.LoginProvider}.com";

                var user = IdentityHelper.CreateUser();
                user.UserName = info.ProviderKey;
                user.Email = email;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.ProfilePicture = profilePicture;

                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Memeber);
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }

            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/" || !Url.IsLocalUrl(returnUrl))
            {
                var redirectUrl = _userRedirectionService.GetRedirectUrlAfterLoginAsync(User).Result;
                return Redirect(redirectUrl);
            }
            return LocalRedirect(returnUrl);
        }


        [Authorize]
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            TempData["success"] = "You have successfully logged out.";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return RedirectToAction(
                actionName: "HttpStatusCodeHandler",
                controllerName: "Error",
                routeValues: new { StatusCode = 403 }
                );
        }

        #endregion

        #region Password Rest Functionality

        [HttpGet, AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                var callbackUrl = Url.Action("ResetPassword", "Account",
                    new { userId = user.Id, token }, Request.Scheme);

                var subject = "Reset Password";

                _passwordResetEmailTemplate.UserName = user.FullName;
                _passwordResetEmailTemplate.CallbackUrl = HtmlEncoder.Default.Encode(callbackUrl!);

                var message = _passwordResetEmailTemplate.TransformText();

                await _emailService.SendSingleEmailAsync(user.FullName, user.Email!, subject, message);
            }

            return RedirectToAction(nameof(ForgotPasswordConfirmation), "Account",
               new { email = model.Email });
        }

        [HttpGet, AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation(string email)
        {
            var model = new ForgotPasswordModel
            {
                Email = email
            };

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync(string userId, string token)
        {
            var model = new ResetPasswordModel
            {
                Token = token
            };

            if (userId == null || token == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Invalid password reset link.";
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = $"Unable to load user with ID {userId}";
            }
            model.Email = user?.Email;

            return View(model);
        }


        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Unable to load user.";
                return View(model);
            }

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
            var resetPassResult = await _userManager.ResetPasswordAsync(user,
                token, model.Password);

            if (!resetPassResult.Succeeded)
            {
                model.IsSuccess = false;
                model.ErrorMessage = $"Password reset failed. Link invalid or expired.";
                model.Token = null;
                return View(model);
            }

            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var redirectUrl = string.IsNullOrEmpty(model.Area)
                      ? Url.Action("Settings", "Home")
                      : Url.Action("Settings", "Dashboard", new { area = model.Area });
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return Redirect(redirectUrl!);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Login");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = string.Join(" ", result.Errors.Select(e => e.Description));
                return Redirect(redirectUrl!);
            }

            await _signInManager.RefreshSignInAsync(user);

            var subject = "Your Password Has Been Changed";
            var template = _passwordChangeEmailTemplate;
            template.UserName = user.UserName;
            template.DateValue = _serverTime.GetCurrentServerTime().ToString("MMMM dd, yyyy HH:mm") + " UTC";
            var message = _passwordChangeEmailTemplate.TransformText();

            await _emailService.SendSingleEmailAsync(user.FirstName + " " + user.LastName, user.Email!, subject, message);

            TempData["SuccessMessage"] = "Your password has been changed successfully. A confirmation email has been sent.";
            return Redirect(redirectUrl!);
        }

        #endregion

        #region Utilities

        private string GetProfilePictureUrl(string provider, ExternalLoginInfo info)
        {
            if (provider == "Google")
                return info.Principal.FindFirst("urn:google:picture")?.Value;

            else if (provider == "Facebook")
            {
                var jsonData = info.Principal.FindFirst("urn:facebook:picture")?.Value;
                // Regex expression for matching
                var regex = new Regex(@"""url"":""([^""]+)""");
                var match = regex.Match(jsonData);

                var url = match.Groups[1].Value;

                return url.Replace("\\", "");
            }
            else
                return "~/sneat/assets/img/avatars/0.png";
        }

        private async Task SendEmailConfirmationAsync(ApplicationUser user, string? returnUrl)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = Url.Action("ConfirmEmail", "Account",
                values: new { area = "", userId = user.Id, code, returnUrl },
                protocol: Request.Scheme);

            var subject = "Confirm your email";

            _accountConfirmationEmailTemplate.UserName = user.FullName;
            _accountConfirmationEmailTemplate.CallbackUrl = HtmlEncoder.Default.Encode(callbackUrl!);
            var message = _accountConfirmationEmailTemplate.TransformText();

            await _emailService.SendSingleEmailAsync(user.FullName, user.Email!, subject, message);
        }

        #endregion
    }
}
