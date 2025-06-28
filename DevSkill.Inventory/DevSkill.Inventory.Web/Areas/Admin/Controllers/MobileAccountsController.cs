using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.MobileAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MobileAccountsController : Controller
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly ILogger<MobileAccountsController> _logger;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public MobileAccountsController(IMapper mapper,
            ILogger<MobileAccountsController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        #endregion

        #region Index AddMobileAccount UpdateMobileAccount RemoveMobileAccount
        public async Task<IActionResult> Index()
        {
            var getMobileAccountListQuery = new GetMobileAccountListQuery();
            var mobileAccounts = await _mediator.Send(getMobileAccountListQuery);

            var model = new MobileAccountListModel();
            model.AddMobileAccountModel.StatusId = (int)Status.Active;
            model.AddMobileAccountModel.Status = EnumHelper.PrepareSelectList<Status>();

            model.UpdateMobileAccountModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var mobileAccount in mobileAccounts)
            {
                var mobileAccountModel = _mapper.Map<MobileAccountModel>(mobileAccount);
                mobileAccountModel.Status = ((Status)mobileAccount.StatusId).ToString();

                model.MobileAccounts.Add(mobileAccountModel);
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMobileAccount(AddMobileAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CurrentBalance = model.OpeningBalance;
                    var mobileAccount = _mapper.Map<MobileAccountAddCommand>(model);
                    await _mediator.Send(mobileAccount);

                    TempData["success'"] = "MobileAccount created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create mobileAccount");
                }
            }
            TempData["error"] = "Failed to create mobileAccount.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMobileAccount(UpdateMobileAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CurrentBalance += model.OpeningBalance;
                    var mobileAccount = _mapper.Map<UpdateMobileAccountCommand>(model);
                    await _mediator.Send(mobileAccount);
                    TempData["success'"] = "MobileAccount updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update mobileAccount");
                }
            }
            TempData["error"] = "Failed to update mobileAccount.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveMobileAccount(Guid id)
        {
            try
            {
                var mobileAccount = new MobileAccountDeleteCommand(id);
                await _mediator.Send(mobileAccount);

                TempData["success'"] = "MobileAccount deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete mobileAccount");
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> GetMobileAccountDataById(string mobileAccountId)
        {
            var getMobileAccountByIdQuery = new GetMobileAccountByIdQuery(Guid.Parse(mobileAccountId));
            var mobileAccount = await _mediator.Send(getMobileAccountByIdQuery);

            return Json(mobileAccount);
        }

        #endregion
    }
}
