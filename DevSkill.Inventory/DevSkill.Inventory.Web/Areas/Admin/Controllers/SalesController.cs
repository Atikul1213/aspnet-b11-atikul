using AutoMapper;
using DevSkill.Inventory.Application.Features.Customers.Queries;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Application.Features.SalesProduct.Commands;
using DevSkill.Inventory.Application.Features.SalesProduct.Queries;
using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries;
using DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Customers;
using DevSkill.Inventory.Web.Areas.Admin.Models.Sales;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SalesController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        #endregion

        #region Ctor
        public SalesController(IMediator mediator,
            ILogger<SalesController> logger,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IApplicationUnitOfWork applicationUnitOfWork)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region SalesIndex / AddSales / UpdateSales / ShowSales / DeleteSales

        [Authorize(Roles = "Admin,Registered")]
        public async Task<IActionResult> SalesIndex()
        {
            var model = new SalesListModel();

            model.SearchItem.DateFrom = new DateTime(2025, 1, 1);
            model.SearchItem.DateTo = new DateTime(2025, 1, 1);
            model.Status = EnumHelper.PrepareSelectList<SalesStatus>();
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSales()
        {
            var model = new AddSalesModel();

            var customers = await _mediator.Send(new GetActiveCustomerListQuery());
            var products = await _mediator.Send(new GetProductListQuery());

            var customerSelectList = EnumHelper.PrepareSelectListFromEntities(customers, c => c.Id, c => c.Name);

            model.Customers = customerSelectList;

            var productSelectList = EnumHelper.PrepareSelectListFromEntities(products, c => c.Id, c => c.Name);

            var accountSelectList = new List<SelectListItem>();

            model.Accounts = accountSelectList;

            model.Products = productSelectList;

            var salesTypeSelectList = EnumHelper.PrepareSelectList<SalesType>();

            model.SalesTypes = salesTypeSelectList;

            var accountTypeSelectList = EnumHelper.PrepareSelectList<AccountType>();

            model.AccountTypes = accountTypeSelectList;

            Random random = new Random();
            int threeDigitNumber = random.Next(100, 1000);
            model.InvoiceNo = $"INV-SUN000{threeDigitNumber}";
            model.SaleDate = new DateTime(2025, 1, 1);

            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSales(AddSalesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = await _mediator.Send(new GetCustomerByIdQuery(model.CustomerId));
                    model.CustomerName = customer?.Name;
                    model.CustomerPhoneNumber = customer?.MobileNumber;
                    if (model.DueAmount == 0)
                    {
                        model.StatusId = (int)SalesStatus.FullPaid;
                    }
                    else
                    {
                        model.StatusId = (int)SalesStatus.Due;
                    }

                    var salesAddCommand = _mapper.Map<SalesAddCommand>(model);

                    var isEnougBalance = await HandleBalanceTransfer(model);

                    if (!isEnougBalance)
                    {
                        TempData["error"] = "You don't have enough balance. Please try again later.";
                        return RedirectToAction("SalesIndex");
                    }

                    var sales = await _mediator.Send(salesAddCommand);

                    if (sales is not null && sales.Id != Guid.Empty && model.SaleProducts is not null && model.SaleProducts.Count > 0)
                    {
                        foreach (var saleProduct in model.SaleProducts)
                        {
                            saleProduct.SalesId = sales.Id;

                            var saleProductEntity = _mapper.Map<SaleProduct>(saleProduct);
                            await _applicationUnitOfWork.SaleProductRepository.AddAsync(saleProductEntity);
                            await _applicationUnitOfWork.SaveAsync();

                            var product = await _applicationUnitOfWork.ProductRepository.GetByIdAsync(saleProduct.ProductId);

                            if (product is not null)
                            {
                                product.Stock -= saleProduct.Quantity;

                                if (model.SalesTypeId == (int)SalesType.WholeSales)
                                    product.WholeSalePrice += saleProduct.SubTotal;

                                if (model.SalesTypeId == (int)SalesType.MRPSales)
                                    product.PurchasePrice += saleProduct.SubTotal;

                                await _applicationUnitOfWork.ProductRepository.UpdateAsync(product);
                                await _applicationUnitOfWork.SaveAsync();
                            }

                        }
                    }


                    TempData["success"] = "Sales created successfully";
                    return RedirectToAction("SalesIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to create sales.";
                _logger.LogError(ex, "There was an error while creating sales");
            }

            return RedirectToAction("SalesIndex");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSales(Guid id)
        {
            try
            {
                var sales = await _mediator.Send(new GetSalesByIdQuery(id));

                if (sales is null)
                    return RedirectToAction("SalesIndex");

                var saleProducts = await _applicationUnitOfWork.SaleProductRepository.GetSaleProductsBySaleIdAsync(sales.Id);
                var model = _mapper.Map<UpdateSalesModel>(sales);

                if (saleProducts is not null && saleProducts.Count > 0)
                {
                    foreach (var product in saleProducts)
                    {
                        var productModel = _mapper.Map<UpdateSalesProductModel>(product);
                        model.SaleProducts.Add(productModel);
                    }
                }

                var customers = await _mediator.Send(new GetActiveCustomerListQuery());
                var products = await _mediator.Send(new GetProductListQuery());

                var customerSelectList = EnumHelper.PrepareSelectListFromEntities(customers, c => c.Id, c => c.Name);

                model.Customers = customerSelectList;

                var productSelectList = EnumHelper.PrepareSelectListFromEntities(products, c => c.Id, c => c.Name);

                model.Products = productSelectList;

                model.SalesTypes = EnumHelper.PrepareSelectList<SalesType>();
                model.AccountTypes = EnumHelper.PrepareSelectList<AccountType>();
                model.SaleDate = new DateTime(2025, 1, 1);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = "No sales found with the Id";
                _logger.LogError(ex, "No sales found with the Id");
            }

            return RedirectToAction("SalesIndex");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSales(UpdateSalesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = await _mediator.Send(new GetCustomerByIdQuery(model.CustomerId));
                    model.CustomerName = customer?.Name;
                    model.CustomerPhoneNumber = customer?.MobileNumber;
                    if (model.DueAmount == 0)
                    {
                        model.StatusId = (int)SalesStatus.FullPaid;
                    }
                    else
                    {
                        model.StatusId = (int)SalesStatus.Due;
                    }

                    var salesUpdateCommand = _mapper.Map<SalesUpdateCommand>(model);

                    var addSalesModel = _mapper.Map<AddSalesModel>(model);

                    var isEnougBalance = await HandleBalanceTransfer(addSalesModel);

                    if (!isEnougBalance)
                    {
                        TempData["error"] = "You don't have enough balance. Please try again later.";
                        return RedirectToAction("SalesIndex");
                    }

                    var sales = await _mediator.Send(salesUpdateCommand);

                    if (sales is not null && sales.Id != Guid.Empty && model.SaleProducts is not null && model.SaleProducts.Count > 0)
                    {
                        foreach (var saleProduct in model.SaleProducts)
                        {
                            saleProduct.SalesId = sales.Id;

                            var saleProductEntity = _mapper.Map<SaleProduct>(saleProduct);
                            await _applicationUnitOfWork.SaleProductRepository.AddAsync(saleProductEntity);
                            await _applicationUnitOfWork.SaveAsync();

                            var product = await _applicationUnitOfWork.ProductRepository.GetByIdAsync(saleProduct.ProductId);

                            if (product is not null)
                            {
                                product.Stock -= saleProduct.Quantity;

                                if (model.SalesTypeId == (int)SalesType.WholeSales)
                                    product.WholeSalePrice += saleProduct.SubTotal;

                                if (model.SalesTypeId == (int)SalesType.MRPSales)
                                    product.PurchasePrice += saleProduct.SubTotal;

                                await _applicationUnitOfWork.ProductRepository.UpdateAsync(product);
                                await _applicationUnitOfWork.SaveAsync();
                            }

                        }
                    }

                    TempData["success"] = "Sales updated successfully";
                    return RedirectToAction("SalesIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to create sales.";
                _logger.LogError(ex, "There was an error while creating sales");
            }

            return RedirectToAction("SalesIndex");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowSales(Guid id)
        {
            try
            {
                var sales = await _mediator.Send(new GetSalesByIdQuery(id));

                if (sales is null)
                    return RedirectToAction("SalesIndex");

                var model = _mapper.Map<ShowSalesModel>(sales);
                var customer = await _mediator.Send(new GetCustomerByIdQuery(sales.CustomerId));
                var saleProducts = await _applicationUnitOfWork.SaleProductRepository.GetSaleProductsBySaleIdAsync(sales.Id);

                if (saleProducts is not null && saleProducts.Count > 0)
                {
                    foreach (var product in saleProducts)
                    {
                        var productModel = _mapper.Map<SalesProductModel>(product);
                        model.SaleProducts.Add(productModel);
                    }
                }

                if (customer != null)
                {
                    var customerModel = _mapper.Map<CustomerModel>(customer);

                    model.Customer = customerModel;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = "No sales found with the Id";
                _logger.LogError(ex, "No sales found with the Id");
            }

            return RedirectToAction("SalesIndex");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSales(Guid id)
        {
            try
            {
                var salesDeleteCommand = new SalesDeleteCommand(id);

                await _mediator.Send(salesDeleteCommand);
                ViewData["success"] = "Sales deleted successfully";
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Failed to delete sales";
                _logger.LogError(ex, "There was an error while deleting sales");
            }

            return RedirectToAction("SalesIndex");
        }

        [HttpPost]
        public async Task<IActionResult> GetCQRSSalesSPJsonData([FromBody] GetAllSalesQuery model)
        {
            try
            {
                var (data, total, totalDisplay) = await _mediator.Send(model);

                var saless = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.InvoiceNo),
                                HttpUtility.HtmlEncode(record.SaleDate.ToString("dd-MM-yyyy")),
                                HttpUtility.HtmlEncode($"{record.CustomerName} {record.CustomerPhoneNumber}"),
                                record.TotalAmount.ToString("C"),
                                record.PaidAmount.ToString("C"),
                                record.DueAmount.ToString("C"),
                                HttpUtility.HtmlEncode(((SalesStatus)record.StatusId).ToString()),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(saless);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting sales data");

                return Json(DataTables.EmptyResult);
            }
        }

        #endregion

        #region Utilities

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
            return result;
        }


        [HttpPost]
        public async Task<IActionResult> GetProductInfoJsonData(string productId)
        {
            var saleProductId = Guid.Parse(productId);

            var product = await _mediator.Send(new GetProductByIdQuery(saleProductId));

            var result = _mapper.Map<AddSalesProductModel>(product);
            result.ProductId = product.Id;
            result.Quantity = 1;
            result.SubTotal = product.MRPPrice * result.Quantity;
            return Json(result);
        }

        private async Task<bool> HandleBalanceTransfer(AddSalesModel model)
        {

            switch (model.AccountTypeId)
            {
                case (int)AccountType.Bank:
                    var sendingbankAccont = await _mediator.Send(new GetBankAccountByIdQuery(model.AccountNoId));
                    if (sendingbankAccont.CurrentBalance < model.TotalAmount)
                        return false;

                    sendingbankAccont.CurrentBalance -= model.TotalAmount;
                    await _applicationUnitOfWork.BankAccountRepository.UpdateAsync(sendingbankAccont);
                    await _applicationUnitOfWork.SaveAsync();
                    break;

                case (int)AccountType.Mobile:

                    var sendingMobileAccont = await _mediator.Send(new GetMobileAccountByIdQuery(model.AccountNoId));
                    if (sendingMobileAccont.CurrentBalance < model.TotalAmount)
                        return false;

                    sendingMobileAccont.CurrentBalance -= model.TotalAmount;
                    await _applicationUnitOfWork.MobileAccountRepository.UpdateAsync(sendingMobileAccont);
                    await _applicationUnitOfWork.SaveAsync();

                    break;


                case (int)AccountType.Cash:
                    var sendingCashAccont = await _mediator.Send(new GetCashAccountByIdQuery(model.AccountNoId));
                    if (sendingCashAccont.CurrentBalance < model.TotalAmount)
                        return false;

                    sendingCashAccont.CurrentBalance -= model.TotalAmount;
                    await _applicationUnitOfWork.CashAccountRepository.UpdateAsync(sendingCashAccont);
                    await _applicationUnitOfWork.SaveAsync();
                    break;
            }

            return true;
        }


        #endregion
    }
}
