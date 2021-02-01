using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Mvc.Routing.Localization.Attributes;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kentico.Kontent.Delivery.Urls.QueryParameters.Filters;
using KenticoKontentModels;
using Microsoft.AspNetCore.Mvc;

using static Kontent_MVC_Navigation.Configuration.Constants;

namespace Kontent_MVC_Navigation.Controllers
{

    [LocalizedRoute(EnglishCulture, "Products")]
    [LocalizedRoute(SpanishCulture, "Productos")]
    public class ProductsController : Controller
    {
        private readonly IDeliveryClient _deliveryClient;

        public ProductsController(IDeliveryClient deliveryClient)
        {
            _deliveryClient = deliveryClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _deliveryClient.GetItemAsync<Page>("products", new DepthParameter(2),
                new EqualsFilter("system.language", CultureInfo.CurrentCulture.Name), // disable language fallback
                new LanguageParameter(CultureInfo.CurrentCulture.Name)
                );

            var products = response.Item;

            if (products != null)
            {
                return View(products);
            }
            else
            {
                return NotFound();
            }
        }
    }
}