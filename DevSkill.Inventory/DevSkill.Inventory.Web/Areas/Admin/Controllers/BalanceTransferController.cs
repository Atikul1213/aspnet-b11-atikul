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

                    var balanceTransfer = _mapper.Map<BalanceTransferAddCommand>(model);
                    await _mediator.Send(balanceTransfer);

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
