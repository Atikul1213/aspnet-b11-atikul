using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.BankAccounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BankAccountsController : Controller
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly ILogger<BankAccountsController> _logger;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public BankAccountsController(IMapper mapper,
            ILogger<BankAccountsController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        #endregion

        #region Index AddBankAccount UpdateBankAccount RemoveBankAccount

        [Authorize(Roles = "Admin,Registered")]
        public async Task<IActionResult> Index()
        {
            var getBankAccountListQuery = new GetBankAccountListQuery();
            var bankAccounts = await _mediator.Send(getBankAccountListQuery);

            var model = new BankAccountListModel();
            model.AddBankAccountModel.StatusId = (int)Status.Active;
            model.AddBankAccountModel.Status = EnumHelper.PrepareSelectList<Status>();

            model.UpdateBankAccountModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var bankAccount in bankAccounts)
            {
                var bankAccountModel = _mapper.Map<BankAccountModel>(bankAccount);
                bankAccountModel.Status = ((Status)bankAccount.StatusId).ToString();

                model.BankAccounts.Add(bankAccountModel);
            }

            return View(model);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBankAccount(AddBankAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CurrentBalance = model.OpeningBalance;
                    var bankAccount = _mapper.Map<BankAccountAddCommand>(model);
                    await _mediator.Send(bankAccount);

                    TempData["success"] = "BankAccount created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create bankAccount");
                }
            }
            TempData["error"] = "Failed to create bankAccount.";

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBankAccount(UpdateBankAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bankAccount = _mapper.Map<UpdateBankAccountCommand>(model);
                    await _mediator.Send(bankAccount);
                    TempData["success"] = "BankAccount updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update bankAccount");
                }
            }
            TempData["error"] = "Failed to update bankAccount.";

            return RedirectToAction("Index");
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveBankAccount(Guid id)
        {
            try
            {
                var bankAccount = new BankAccountDeleteCommand(id);
                await _mediator.Send(bankAccount);

                TempData["success"] = "BankAccount deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete bankAccount");
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> GetBankAccountDataById(string bankAccountId)
        {
            var getBankAccountByIdQuery = new GetBankAccountByIdQuery(Guid.Parse(bankAccountId));
            var bankAccount = await _mediator.Send(getBankAccountByIdQuery);

            return Json(bankAccount);
        }

        #endregion
    }
}
