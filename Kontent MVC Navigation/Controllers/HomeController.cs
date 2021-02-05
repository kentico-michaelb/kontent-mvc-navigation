using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kontent_MVC_Navigation.Models;
using Kentico.Kontent.Delivery.Abstractions;
using AspNetCore.Mvc.Routing.Localization.Attributes;

using static Kontent_MVC_Navigation.Configuration.Constants;

namespace Kontent_MVC_Navigation.Controllers
{
    [LocalizedRoute(EnglishCulture, "home")]
    [LocalizedRoute(SpanishCulture, "inicio")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IDeliveryClient _deliveryClient;

        public HomeController(ILogger<HomeController> logger, IDeliveryClient deliveryClient)
        {
            _logger = logger;
            _deliveryClient = deliveryClient;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
