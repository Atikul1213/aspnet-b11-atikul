using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands
{
    public class BankAccountAddCommand : IRequest
    {
        public string Name { get; set; }
        public int AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int StatusId { get; set; }
    }
}
