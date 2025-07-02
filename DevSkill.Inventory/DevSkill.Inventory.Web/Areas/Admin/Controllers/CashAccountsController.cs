using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CashAccountsController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<CashAccountsController> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public CashAccountsController(IMapper mapper,
            ILogger<CashAccountsController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddCashAccount UpdateCashAccount RemoveCashAccount
        public async Task<IActionResult> Index()
        {
            var getCashAccountListQuery = new GetCashAccountListQuery();
            var cashAccounts = await _mediator.Send(getCashAccountListQuery);

            var model = new CashAccountListModel();
            model.AddCashAccountModel.StatusId = (int)Status.Active;
            model.AddCashAccountModel.Status = EnumHelper.PrepareSelectList<Status>();

            model.UpdateCashAccountModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var cashAccount in cashAccounts)
            {
                var cashAccountModel = _mapper.Map<CashAccountModel>(cashAccount);
                cashAccountModel.Status = ((Status)cashAccount.StatusId).ToString();

                model.CashAccounts.Add(cashAccountModel);
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCashAccount(AddCashAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CurrentBalance = model.Balance;
                    var cashAccount = _mapper.Map<CashAccountAddCommand>(model);
                    await _mediator.Send(cashAccount);

                    TempData["success"] = "CashAccount created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create cashAccount");
                }
            }
            TempData["error"] = "Failed to create cashAccount.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCashAccount(UpdateCashAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CurrentBalance += model.Balance;
                    var cashAccount = _mapper.Map<UpdateCashAccountCommand>(model);
                    await _mediator.Send(cashAccount);
                    TempData["success"] = "CashAccount updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update cashAccount");
                }
            }
            TempData["error"] = "Failed to update cashAccount.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveCashAccount(Guid id)
        {
            try
            {
                var cashAccount = new CashAccountDeleteCommand(id);
                await _mediator.Send(cashAccount);

                TempData["success"] = "CashAccount deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete cashAccount");
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
