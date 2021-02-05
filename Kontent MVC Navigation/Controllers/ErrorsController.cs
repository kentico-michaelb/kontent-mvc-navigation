using AspNetCore.Mvc.Routing.Localization.Attributes;
using Microsoft.AspNetCore.Mvc;

using static Kontent_MVC_Navigation.Configuration.Constants;

namespace Kontent_MVC_Navigation.Controllers
{
    [LocalizedRoute(EnglishCulture, "errors")]
    [LocalizedRoute(SpanishCulture, "errores")]
    public class ErrorsController : Controller
    {
        public new IActionResult NotFound()
        {
            return View();
        }
    }
}