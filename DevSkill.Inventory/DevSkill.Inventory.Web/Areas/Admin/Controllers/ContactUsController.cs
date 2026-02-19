using AutoMapper;
using Cortex.Mediator;
using DevSkill.Core.Application;
using DevSkill.Inventory.Application.Features.ContactUs.GetContactUsInfo;
using DevSkill.Inventory.Application.Features.ContactUs.UpsertContactUsInfo;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ContactUsController : Controller
    {
        #region Fields
        private readonly ILogger<ContactUsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public ContactUsController(ILogger<ContactUsController> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }
        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                var contactUsInfo = await _mediator.SendQueryAsync<GetContactUsInfoQuery, ContactUsInfo>(new GetContactUsInfoQuery());
                var model = _mapper.Map<ContactUsModel>(contactUsInfo);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load contact info.");
                TempData["error"] = "Failed to load contact info.";

                return View(new ContactUsModel());
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactUsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var upsertContactUsInfoCommand = _mapper.Map<UpsertContactUsInfoCommand>(model);
            try
            {
                var result = await _mediator.SendCommandAsync<UpsertContactUsInfoCommand, ResultResponse<ContactUsInfo>>(upsertContactUsInfoCommand);

                if (result.IsSuccess)
                {
                    TempData["success"] = "Contact us content saved.";

                    return RedirectToAction(nameof(Edit));
                }

                TempData["error"] = string.Join(" ", result.Errors);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save contact us info.");
                TempData["error"] = "An unexpected error occurred while saving.";

                return View(model);
            }
        }

        #endregion
    }
}
