using DevSkill.Inventory.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var model = new ErrorViewModel
            {
                StatusCode = statusCode,
                TraceId = HttpContext.TraceIdentifier
            };

            model.Title = statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                402 => "Payment Required",
                403 => "Access Denied",
                404 => "Page not Found",
                408 => "Request Timeout",
                500 => "Internal Server Error",
                503 => "Service Unavailable",
                _ => "Something went wrong"
            };

            model.ErrorMessage = statusCode switch
            {
                400 => "The request could not be understood by the server.",
                401 => "You must be logged in to access this page.",
                402 => "Payment is required to access this resource.",
                403 => "You do not have permission to access this resource.",
                404 => "The page you are looking for does not exist.",
                408 => "The server timed out waiting for your request.",
                500 => "An unexpected server error occurred. Please try again later.",
                503 => "The server is currently unavailable. Please try again later.",
                _ => "An error occurred while processing your request."
            };

            return View("Error", model);
        }


        [Route("Error")]
        public IActionResult ErrorHandler()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var model = new ErrorViewModel
            {
                StatusCode = 500,
                Title = "Unexpected Error",
                ErrorMessage = exceptionFeature?.Error.Message ?? "An unexpected error occurred.",
                TraceId = HttpContext.TraceIdentifier
            };

            return View("Error", model);
        }
    }
}
