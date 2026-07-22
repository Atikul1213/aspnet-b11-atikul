using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Constants;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace DevSkill.Inventory.Infrastructure.Services
{
    public class UserRedirectionService : IUserRedirectionService
    {
        #region Fields
        private readonly ILogger<UserRedirectionService> _logger;
        #endregion

        #region Ctor
        public UserRedirectionService(ILogger<UserRedirectionService> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<string> GetRedirectUrlAfterLoginAsync(IPrincipal user)
        {
            try
            {
                if (user?.Identity?.IsAuthenticated is false)
                {
                    _logger.LogWarning("User is not authenticated. Redirecting to home.");
                    return "/";
                }

                var redirectUrl = user switch
                {
                    _ when user!.IsInRole(ApplicationRoles.Admin) => "/Admin/Products/IndexSP",
                    _ when user!.IsInRole(ApplicationRoles.Memeber) => "/",
                    _ => "/"
                };

                _logger.LogInformation($"User redirected to {redirectUrl}");
                return await Task.FromResult(redirectUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error determining redirect URL for user");
                return "/";
            }
        }

        #endregion
    }
}
