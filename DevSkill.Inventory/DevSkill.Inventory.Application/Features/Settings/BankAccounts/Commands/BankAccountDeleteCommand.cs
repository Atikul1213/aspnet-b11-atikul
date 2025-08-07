using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands
{
    public class BankAccountDeleteCommand : IRequest
    {
        public BankAccountDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
