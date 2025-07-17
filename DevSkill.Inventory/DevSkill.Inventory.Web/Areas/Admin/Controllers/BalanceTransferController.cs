using AutoMapper;
using DevSkill.Inventory.Application.Features.BalanceTransfers.Queries;
using DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Commands;
using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries;
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
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public BalanceTransferController(IMapper mapper,
            ILogger<BalanceTransferController> logger,
            IMediator mediator,
            IWebHostEnvironment webHostEnvironment,
            IApplicationUnitOfWork applicationUnitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Index AddBalanceTransfer RemoveBalanceTransfer
        public async Task<IActionResult> Index()
        {
            var model = new BalanceTransferListModel();

            model.AddBalanceTransferModel.AccountTypes = EnumHelper.PrepareSelectList<AccountType>();

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

                    var result = await HandleBalanceTransfer(model);
                    if (result == false)
                    {
                        TempData["success"] = "Insufficient account balance.";

                        return RedirectToAction("Index");
                    }


                    var balanceTransfer = _mapper.Map<BalanceTransferAddCommand>(model);

                    await _mediator.Send(balanceTransfer);

                    TempData["success"] = "Balance Transfer successfully.";

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

        private async Task<bool> HandleBalanceTransfer(AddBalanceTransferModel model)
        {

            switch (model.SendingAccountTypeId)
            {
                case (int)AccountType.Bank:
                    var sendingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.SendingAccountId));
                    if (sendingbankAccont.CurrentBalance < model.TransferAmount)
                        return false;

                    sendingbankAccont.CurrentBalance -= model.TransferAmount;
                    await _applicationUnitOfWork.BankAccountRepository.UpdateAsync(sendingbankAccont);
                    await _applicationUnitOfWork.SaveAsync();

                    switch (model.ReceiveAccountTypeId)
                    {
                        case (int)AccountType.Bank:
                            var receivingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.ReceiveAccountId));
                            receivingbankAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.BankAccountRepository.UpdateAsync(sendingbankAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;
                        case (int)AccountType.Mobile:
                            var receivingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.ReceiveAccountId));
                            receivingMobileAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.MobileAccountRepository.UpdateAsync(receivingMobileAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;

                        case (int)AccountType.Cash:
                            var receivingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.ReceiveAccountId));
                            receivingCashAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.CashAccountRepository.UpdateAsync(receivingCashAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;
                    }
                    break;



                case (int)AccountType.Mobile:

                    var sendingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.SendingAccountId));
                    if (sendingMobileAccont.CurrentBalance < model.TransferAmount)
                        return false;

                    sendingMobileAccont.CurrentBalance -= model.TransferAmount;
                    await _applicationUnitOfWork.MobileAccountRepository.UpdateAsync(sendingMobileAccont);
                    await _applicationUnitOfWork.SaveAsync();

                    switch (model.ReceiveAccountTypeId)
                    {
                        case (int)AccountType.Bank:
                            var receivingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.ReceiveAccountId));
                            receivingbankAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.BankAccountRepository.UpdateAsync(receivingbankAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;
                        case (int)AccountType.Mobile:
                            var receivingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.ReceiveAccountId));
                            receivingMobileAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.MobileAccountRepository.UpdateAsync(sendingMobileAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;

                        case (int)AccountType.Cash:
                            var receivingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.ReceiveAccountId));
                            receivingCashAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.CashAccountRepository.UpdateAsync(receivingCashAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;
                    }
                    break;




                case (int)AccountType.Cash:
                    var sendingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.SendingAccountId));
                    if (sendingCashAccont.CurrentBalance < model.TransferAmount)
                        return false;

                    sendingCashAccont.CurrentBalance -= model.TransferAmount;
                    await _applicationUnitOfWork.CashAccountRepository.UpdateAsync(sendingCashAccont);
                    await _applicationUnitOfWork.SaveAsync();
                    switch (model.ReceiveAccountTypeId)
                    {
                        case (int)AccountType.Bank:
                            var receivingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.ReceiveAccountId));
                            receivingbankAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.BankAccountRepository.UpdateAsync(receivingbankAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;
                        case (int)AccountType.Mobile:
                            var receivingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.ReceiveAccountId));
                            receivingMobileAccont.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.MobileAccountRepository.UpdateAsync(receivingMobileAccont);
                            await _applicationUnitOfWork.SaveAsync();
                            break;

                        case (int)AccountType.Cash:
                            var receivingCashAccount = await _mediator.Send(new GetCashAccountByIdQuery(model.ReceiveAccountId));
                            receivingCashAccount.CurrentBalance += model.TransferAmount;
                            await _applicationUnitOfWork.CashAccountRepository.UpdateAsync(receivingCashAccount);
                            await _applicationUnitOfWork.SaveAsync();
                            break;
                    }
                    break;
            }

            return true;
        }


        public async Task<IActionResult> RemoveBalanceTransfer(Guid id)
        {
            try
            {
                var balanceTransferCommand = new BalanceTransferDeleteCommand(id);
                await _mediator.Send(balanceTransferCommand);

                TempData["success"] = "Balance transfer deleted successfully.";
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
