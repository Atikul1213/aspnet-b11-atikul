using DevSkill.Core.Application.UtilitiesContracts;
using DevSkill.Core.Domain;
using DevSkill.Core.Domain.EmailServiceContracts;
using DevSkill.Inventory.Api.Models.JwtToken.Dto;
using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Constants;
using DevSkill.Inventory.Domain.Templates;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Infrastructure.Services;
using DevSkill.Inventory.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace DevSkill.Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<AuthController> _logger;
        private readonly ICaptchaService _captchaService;
        private readonly IUserRedirectionService _userRedirectionService;
        private readonly IEmailService _emailService;
        private readonly IAccountConfirmationEmailTemplate _accountConfirmationEmailTemplate;
        private readonly IPasswordResetEmailTemplate _passwordResetEmailTemplate;
        private readonly IPasswordChangeEmailTemplate _passwordChangeEmailTemplate;
        private readonly IServerTime _serverTime;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        public AuthController(UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthController> logger,
            ICaptchaService captchaService,
            IUserRedirectionService userRedirectionService,
            IEmailService emailService,
            IAccountConfirmationEmailTemplate accountConfirmationEmailTemplate,
            IPasswordResetEmailTemplate passwordResetEmailTemplate,
            IPasswordChangeEmailTemplate passwordChangeEmailTemplate,
            IServerTime serverTime,
            IJwtTokenService jwtTokenService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = IdentityHelper.GetEmailStore(userManager, userStore);
            _signInManager = signInManager;
            _logger = logger;
            _captchaService = captchaService;
            _userRedirectionService = userRedirectionService;
            _emailService = emailService;
            _accountConfirmationEmailTemplate = accountConfirmationEmailTemplate;
            _passwordResetEmailTemplate = passwordResetEmailTemplate;
            _passwordChangeEmailTemplate = passwordChangeEmailTemplate;
            _serverTime = serverTime;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
        }

        #endregion

        #region Login  Register Logout

        [HttpGet("GetToken")]
        public async Task<IActionResult> GetToken(string email, string password)
        {
            if (email != null && password != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

                if (result != null && result.Succeeded)
                {
                    var claims = (await _userManager.GetClaimsAsync(user)).ToArray();
                    var token = await _jwtTokenService.GenerateTokenAsync(claims,
                            _configuration["Jwt:Key"],
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"]
                        );

                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDto model)
        {
            var captchaResult = await _captchaService.VerifyAsync(model.ReCaptchaToken);
            if (!captchaResult.IsValid && false)
            {
                return BadRequest(new { message = "ReCaptcha validation failed. Please try again." });
            }

            ApplicationUser? user = model.EmailOrUserName.Contains('@')
                ? await _userManager.FindByEmailAsync(model.EmailOrUserName)
                : await _userManager.FindByNameAsync(model.EmailOrUserName);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid login attempt." });
            }

            if (!user.EmailConfirmed)
            {
                await SendEmailConfirmationAsync(user, "/");
                return BadRequest(new
                {
                    message = "You must confirm your email address before you can log in. We've sent a new confirmation email to your address."
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = (await _userManager.GetClaimsAsync(user)).ToArray();
                var token = await _jwtTokenService.GenerateTokenAsync(claims,
                        _configuration["Jwt:Key"],
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"]
                    );

                var response = new LoginResponseDto
                {
                    AccessToken = token.AccessToken,
                    ExpiresAtUtc = token.ExpiresAtUtc,
                    RefreshToken = token.RefreshToken,
                    // User = MapToUserInfo(user, roles)
                };

                return Ok(new { response, message = "You have successfully logged in." });
            }

            if (result.IsLockedOut)
            {
                return StatusCode(StatusCodes.Status423Locked,
                    new { message = "Your account has been locked out due to multiple failed login attempts. Please try again later." });
            }

            if (result.IsNotAllowed)
            {
                return BadRequest(new { message = "Your account is not allowed to sign in. Please confirm your email or contact support." });
            }

            if (result.RequiresTwoFactor)
            {
                return Ok(new { RememberMe = model.RememberMe, message = "Requires two factor authentication." });
            }

            return Unauthorized(new { message = "Invalid login attempt." });
        }


        [HttpPost("register"), AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            var user = IdentityHelper.CreateUser();

            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Failed to register. Please correct the errors and try again.",
                    error = result.Errors.Select(e => e.Description)
                });
            }

            await _userManager.AddToRoleAsync(user, ApplicationRoles.Memeber);
            await SendEmailConfirmationAsync(user, "/");

            return Ok(new { Email = user.Email });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "You have successfully logged out." });
        }

        #endregion

        #region Utilities
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
