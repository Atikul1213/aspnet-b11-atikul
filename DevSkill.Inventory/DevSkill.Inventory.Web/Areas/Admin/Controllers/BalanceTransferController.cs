using AutoMapper;
using DevSkill.Inventory.Application.Features.BalanceTransfers.Queries;
using DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Commands;
using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries;
using DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.BalanceTransfers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BalanceTransferController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<BalanceTransferController> _logger;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Ctor
        public BalanceTransferController(IMapper mapper,
            ILogger<BalanceTransferController> logger,
            IMediator mediator,
            IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Index AddBalanceTransfer UpdateBalanceTransfer RemoveBalanceTransfer
        public async Task<IActionResult> Index()
        {
            var model = new BalanceTransferListModel();

            var accountTypeSelectList = EnumHelper.PrepareSelectList<AccountType>();
            accountTypeSelectList.Insert(0, new SelectListItem
            {
                Text = "Select One",
                Value = Guid.Empty.ToString()
            });
            model.AddBalanceTransferModel.AccountTypes = accountTypeSelectList;

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBalanceTransfer(AddBalanceTransferModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.FromAccountName = await GetAccountNameAsync(model.SendingAccountTypeId, model.SendingAccountId);
                    model.ToAccountName = await GetAccountNameAsync(model.ReceiveAccountTypeId, model.ReceiveAccountId);
                    model.TransferDate = DateTime.UtcNow;
                    var balanceTransfer = _mapper.Map<BalanceTransferAddCommand>(model);

                    await _mediator.Send(balanceTransfer);
                    await HandleBalanceTransfer(model);

                    TempData["success'"] = "Balance Transfer successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to transfer balance");
                }
            }
            TempData["error"] = "Failed to transfer balance.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetCQRSBalanceTransferJsonData([FromBody] GetBalanceTransferListQuery model)
        {
            try
            {
                var result = await _mediator.Send(model);

                var balanceTransfers = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.TransferDate.ToString("dd-MM-yyyy")),
                                HttpUtility.HtmlEncode(record.FromAccountName),
                                HttpUtility.HtmlEncode(record.ToAccountName),
                                HttpUtility.HtmlEncode(record.TransferAmount),
                                HttpUtility.HtmlEncode(record.Note),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(balanceTransfers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting balance Transfer data");

                return Json(DataTables.EmptyResult);
            }
        }

        private async Task HandleBalanceTransfer(AddBalanceTransferModel model)
        {

            switch (model.SendingAccountTypeId)
            {
                case (int)AccountType.Bank:
                    var sendingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.SendingAccountId));
                    sendingbankAccont.CurrentBalance -= model.TransferAmount;
                    var bankAccountCommand = _mapper.Map<UpdateBankAccountCommand>(sendingbankAccont);
                    await _mediator.Send(bankAccountCommand);

                    switch (model.ReceiveAccountTypeId)
                    {
                        case (int)AccountType.Bank:
                            var receivingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.ReceiveAccountId));
                            receivingbankAccont.CurrentBalance += model.TransferAmount;
                            var bankAccountCommandReceive = _mapper.Map<UpdateBankAccountCommand>(receivingbankAccont);
                            await _mediator.Send(bankAccountCommandReceive);
                            break;
                        case (int)AccountType.Mobile:
                            var receivingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.ReceiveAccountId));
                            receivingMobileAccont.CurrentBalance += model.TransferAmount;
                            var mobileAccountCommandReceiver = _mapper.Map<UpdateMobileAccountCommand>(receivingMobileAccont);
                            await _mediator.Send(mobileAccountCommandReceiver);
                            break;

                        case (int)AccountType.Cash:
                            var receivingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.ReceiveAccountId));
                            receivingCashAccont.CurrentBalance -= model.TransferAmount;
                            var cashAccountCommandReceiver = _mapper.Map<UpdateCashAccountCommand>(receivingCashAccont);
                            await _mediator.Send(cashAccountCommandReceiver);
                            break;
                    }
                    break;



                case (int)AccountType.Mobile:

                    var sendingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.SendingAccountId));
                    sendingMobileAccont.CurrentBalance -= model.TransferAmount;
                    var mobileAccountCommand = _mapper.Map<UpdateMobileAccountCommand>(sendingMobileAccont);
                    await _mediator.Send(mobileAccountCommand);

                    switch (model.ReceiveAccountTypeId)
                    {
                        case (int)AccountType.Bank:
                            var receivingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.ReceiveAccountId));
                            receivingbankAccont.CurrentBalance += model.TransferAmount;
                            var bankAccountCommandReceive = _mapper.Map<UpdateBankAccountCommand>(receivingbankAccont);
                            await _mediator.Send(bankAccountCommandReceive);
                            break;
                        case (int)AccountType.Mobile:
                            var receivingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.ReceiveAccountId));
                            receivingMobileAccont.CurrentBalance += model.TransferAmount;
                            var mobileAccountCommandReceive = _mapper.Map<UpdateMobileAccountCommand>(receivingMobileAccont);
                            await _mediator.Send(mobileAccountCommandReceive);
                            break;

                        case (int)AccountType.Cash:
                            var receivingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.ReceiveAccountId));
                            receivingCashAccont.CurrentBalance -= model.TransferAmount;
                            var cashAccountCommandReceive = _mapper.Map<UpdateCashAccountCommand>(receivingCashAccont);
                            await _mediator.Send(cashAccountCommandReceive);
                            break;
                    }
                    break;




                case (int)AccountType.Cash:
                    var sendingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.SendingAccountId));
                    sendingCashAccont.CurrentBalance -= model.TransferAmount;
                    var cashAccountCommand = _mapper.Map<UpdateCashAccountCommand>(sendingCashAccont);
                    await _mediator.Send(cashAccountCommand);

                    switch (model.ReceiveAccountTypeId)
                    {
                        case (int)AccountType.Bank:
                            var receivingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.ReceiveAccountId));
                            receivingbankAccont.CurrentBalance += model.TransferAmount;
                            var bankAccountCommandReceive = _mapper.Map<UpdateBankAccountCommand>(receivingbankAccont);
                            await _mediator.Send(bankAccountCommandReceive);
                            break;
                        case (int)AccountType.Mobile:
                            var receivingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.ReceiveAccountId));
                            receivingMobileAccont.CurrentBalance += model.TransferAmount;
                            var mobileAccountCommandReceive = _mapper.Map<UpdateMobileAccountCommand>(receivingMobileAccont);
                            await _mediator.Send(mobileAccountCommandReceive);
                            break;

                        case (int)AccountType.Cash:
                            var receivingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.ReceiveAccountId));
                            receivingCashAccont.CurrentBalance -= model.TransferAmount;
                            var cashAccountCommandReceive = _mapper.Map<UpdateCashAccountCommand>(receivingCashAccont);
                            await _mediator.Send(cashAccountCommandReceive);
                            break;
                    }
                    break;
            }
        }


        public async Task<IActionResult> RemoveBalanceTransfer(Guid id)
        {
            try
            {
                var balanceTransferCommand = new BalanceTransferDeleteCommand(id);
                await _mediator.Send(balanceTransferCommand);

                TempData["success'"] = "Balance transfer deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete balance transfer");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> GetAccountInfoJsonData(string accountTypeId)
        {
            var result = await PrepareAccountInfoAsync(Convert.ToInt32(accountTypeId));

            return Json(result);
        }


        private async Task<IList<SelectListItem>> PrepareAccountInfoAsync(int accountTypeId)
        {
            var result = new List<SelectListItem>();

            switch (accountTypeId)
            {
                case (int)AccountType.Bank:
                    var bankAccounts = await _mediator.Send(new GetActiveBankAccountListQuery());
                    result = EnumHelper.PrepareSelectListFromEntities(bankAccounts, b => b.Id, b => b.Name);
                    break;
                case (int)AccountType.Mobile:
                    var mobileAccounts = await _mediator.Send(new GetActiveMobileAccountListQuery());
                    result = EnumHelper.PrepareSelectListFromEntities(mobileAccounts, b => b.Id, b => b.Name);
                    break;

                case (int)AccountType.Cash:
                    var cashAccounts = await _mediator.Send(new GetActiveCashAccountListQuery());
                    result = EnumHelper.PrepareSelectListFromEntities(cashAccounts, b => b.Id, b => b.Name);
                    break;
            }

            result.Insert(0, new SelectListItem()
            {
                Text = "Select Account No.",
                Value = Guid.Empty.ToString()
            });

            return result;
        }


        private async Task<string> GetAccountNameAsync(int accountTypeId, Guid id)
        {
            var accountName = string.Empty;

            switch (accountTypeId)
            {
                case (int)AccountType.Bank:
                    var bankAccount = await _mediator.Send(new GetBankAccountByIdQuery(id));
                    accountName = bankAccount.Name;
                    break;
                case (int)AccountType.Mobile:
                    var mobileAccount = await _mediator.Send(new GetMobileAccountByIdQuery(id));
                    accountName = mobileAccount.Name;
                    break;

                case (int)AccountType.Cash:
                    var cashAccount = await _mediator.Send(new GetCashAccountByIdQuery(id));
                    accountName = cashAccount.Name;
                    break;
            }

            return accountName;
        }

        #endregion
    }
}
