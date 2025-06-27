using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Commands
{
    public class UpdateMobileAccountCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string Owner { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int StatusId { get; set; }
    }
}
