using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        this.logger = logger;
    }

    [Route("Error/General")]
    [AllowAnonymous]
    public IActionResult General()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (exceptionFeature?.Error != null)
        {
            ViewBag.StackTrace = exceptionFeature.Error.Message;
        }
        else
        {
            ViewBag.ErrorMessage = "Unknown error occurred.";
            ViewBag.StackTrace = string.Empty;
        }
        return View();
    }

    [Route("Error/{statusCode}")]
    [AllowAnonymous]
    public IActionResult InvalidURL(int statusCode)
    {
        var result = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

        switch (statusCode)
        {
            case 404:
                if (result != null)
                {
                    ViewBag.ErrorMessage = $"You are Finding {result.OriginalPath} its not path of our system";
                    logger.LogWarning($"404 path is {result.OriginalPath} query string {result.OriginalQueryString}");
                }
                else
                {
                    ViewBag.ErrorMessage = "The path you're looking for does not exist.";
                    logger.LogWarning("404 occurred but IStatusCodeReExecuteFeature was null.");
                }
                break;

        }

        return View("InvalidURL");
    }


}
