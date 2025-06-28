using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.BalanceTransfers
{
    public class AddBalanceTransferModel
    {
        public AddBalanceTransferModel()
        {
            AccountTypes = new List<SelectListItem>();
            SendingAccounts = new List<SelectListItem>();
            ReceivingAccounts = new List<SelectListItem>();
        }
        [ValidateNever]
        public string FromAccountName { get; set; }
        [ValidateNever]
        public string ToAccountName { get; set; }
        public int SendingAccountTypeId { get; set; }
        public int ReceiveAccountTypeId { get; set; }
        public Guid SendingAccountId { get; set; }
        public Guid ReceiveAccountId { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime TransferDate { get; set; }
        public string Note { get; set; }
        public IEnumerable<SelectListItem> AccountTypes { get; set; }
        public IEnumerable<SelectListItem> SendingAccounts { get; set; }
        public IEnumerable<SelectListItem> ReceivingAccounts { get; set; }
    }
}
