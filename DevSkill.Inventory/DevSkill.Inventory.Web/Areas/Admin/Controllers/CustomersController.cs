using AutoMapper;
using DevSkill.Inventory.Application.Features.Customers.Commands;
using DevSkill.Inventory.Application.Features.Customers.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Ctor
        public CustomersController(IMapper mapper,
            ILogger<CustomersController> logger,
            IMediator mediator,
            IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Index AddCustomer UpdateCustomer RemoveCustomer
        public async Task<IActionResult> Index()
        {
            var model = new CustomerListModel();

            model.AddCustomerModel.StatusId = (int)Status.Active;
            model.AddCustomerModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.UpdateCustomerModel.Status = EnumHelper.PrepareSelectList<Status>();

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(AddCustomerModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string customerImagePath = Path.Combine(wwwRootPath, @"images\customers");

                        using (var fileStream = new FileStream(Path.Combine(customerImagePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        model.ImageUrl = Path.Combine(@"/images/customers", fileName);
                    }

                    var customer = _mapper.Map<CustomerAddCommand>(model);
                    await _mediator.Send(customer);

                    TempData["success'"] = "Customer created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create customer");
                }
            }
            TempData["error"] = "Failed to create customer.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string customerImagePath = Path.Combine(wwwRootPath, @"images\customers");
                        using (var fileStream = new FileStream(Path.Combine(customerImagePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        model.ImageUrl = Path.Combine(@"/images/customers", fileName);
                    }

                    var customer = _mapper.Map<CustomerUpdateCommand>(model);
                    await _mediator.Send(customer);
                    TempData["success'"] = "Customer updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update customer");
                }
            }
            TempData["error"] = "Failed to update customer.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ShowCustomer(Guid id)
        {
            try
            {
                var customer = await _mediator.Send(new GetCustomerByIdQuery(id));

                if (customer != null)
                {
                    var model = _mapper.Map<UpdateCustomerModel>(customer);
                    model.Status = EnumHelper.PrepareSelectList<Status>();

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to show customer");
                TempData["error"] = "Failed to show customer.";
            }

            return RedirectToAction("Index");
        }




        public async Task<IActionResult> RemoveCustomer(Guid id)
        {
            try
            {
                var customer = new CustomerDeleteCommand(id);
                await _mediator.Send(customer);

                TempData["success'"] = "Customer deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete customer");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetCQRSCustomerJsonData([FromBody] GetCustomerListQuery model)
        {
            try
            {
                var result = await _mediator.Send(model);

                var customers = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.MobileNumber),
                                HttpUtility.HtmlEncode(record.Address),
                                HttpUtility.HtmlEncode(record.Email),
                                HttpUtility.HtmlEncode(record.OpeningBalance),
                                HttpUtility.HtmlEncode(((Status)record.StatusId).ToString()),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting customer data");

                return Json(DataTables.EmptyResult);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerDataById(string customerId)
        {
            var getCustomerByIdQuery = new GetCustomerByIdQuery(Guid.Parse(customerId));
            var customer = await _mediator.Send(getCustomerByIdQuery);

            return Json(customer);
        }

        #endregion
    }
}
