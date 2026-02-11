using System.Security.Principal;

namespace DevSkill.Inventory.Domain.Abstractions
{
    public interface IUserRedirectionService
    {
        Task<string> GetRedirectUrlAfterLoginAsync(IPrincipal user);
    }
}
