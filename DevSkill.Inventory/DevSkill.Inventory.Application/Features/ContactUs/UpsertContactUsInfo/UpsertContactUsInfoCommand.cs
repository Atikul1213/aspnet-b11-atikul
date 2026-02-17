using Cortex.Mediator.Commands;
using DevSkill.Core.Application;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.ContactUs.UpsertContactUsInfo
{
    public class UpsertContactUsInfoCommand : ICommand<ResultResponse<ContactUsInfo>>
    {
        public Guid Id { get; set; }
        public string HeaderTitle { get; set; } = string.Empty;
        public string HeaderSubtitle { get; set; } = string.Empty;
        public string HeaderDescription { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string OpenHours { get; set; } = string.Empty;
        public string MapEmbedUrl { get; set; } = string.Empty;
    }
}
