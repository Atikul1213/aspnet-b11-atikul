using AutoMapper;
using Cortex.Mediator;
using DevSkill.Core.Application;
using DevSkill.Inventory.Application.Features.ContactUs.GetContactUsInfo;
using DevSkill.Inventory.Application.Features.ContactUs.SendMessage;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public HomeController(ILogger<HomeController> logger,
            IMapper mapper,
            IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ContactUs()
        {
            var contactUsInfo = await _mediator.SendQueryAsync<GetContactUsInfoQuery, ContactUsInfo>(new GetContactUsInfoQuery());
            var model = new ContactUsInfoModel();

            if (contactUsInfo != null)
            {
                model = _mapper.Map<ContactUsInfoModel>(contactUsInfo);
            }

            ApplyContactDefaults(model);

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsInfoModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var recaptchaToken = Request.Form["g-recaptcha-response"].ToString();

            var command = _mapper.Map<SendContactUsMessageCommand>(model);
            command.RecaptchaToken = recaptchaToken;

            var result = await _mediator.SendCommandAsync<SendContactUsMessageCommand, ResultResponse>(command);
            if (result.IsSuccess)
            {
                TempData["success"] = "Your message has been sent successfully";
                return RedirectToAction(nameof(ContactUs));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            ApplyContactDefaults(model);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }

        #endregion

        #region Utilities

        private static void ApplyContactDefaults(ContactUsInfoModel model)
        {
            if (string.IsNullOrWhiteSpace(model.HeaderTitle))
                model.HeaderTitle = "Contact Us";
            if (string.IsNullOrWhiteSpace(model.HeaderSubtitle))
                model.HeaderSubtitle = "We're Here to Help You Succeed";
            if (string.IsNullOrWhiteSpace(model.HeaderDescription))
                model.HeaderDescription = "Any question or remark? Just write us a message.";
        }

        #endregion
    }
}
