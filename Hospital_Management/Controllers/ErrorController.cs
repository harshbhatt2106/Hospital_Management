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

    [Route("/Error/General")]
    [AllowAnonymous]

    public IActionResult General()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        logger.LogError($"the path {exceptionFeature.Path} throw execption {exceptionFeature.Path}");
        ViewBag.messgae = $"the path {exceptionFeature.Path} throw execption {exceptionFeature.Path} error messgae is {exceptionFeature.Error.Message}";
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
                ViewBag.ErrorMessage = $"You are Finding {result.OriginalPath} its not path of our system";
                logger.LogWarning($"404 path is {result.OriginalPath} query string {result.OriginalQueryString}");
                break;
        }

        return View("InvalidURL");
    }

}
