using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class ErrorController : Controller
    {      
        [Route("Error/General")]
        [AllowAnonymous]
        public IActionResult General()
        {
            var execption = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (execption != null)
            {
                ViewBag.path = execption.Path;
                ViewBag.StackTrace = execption.Error.StackTrace;
                return View("Error");
            }
            else
            {
                ViewBag.path = "execption.Path not found";
                ViewBag.StackTrace = "execption.Error.StackTrace not found";
                return View("Error");
            }
        }
        
        [Route("Error/InvalidUrl")]
        public IActionResult InvalidUrl()
        {
            var data = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            ViewBag.URl_Name= data.OriginalPath;
            return View();
        }
    }
}
