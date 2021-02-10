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
using Kontent_MVC_Navigation.Models;
using Microsoft.AspNetCore.Mvc;

using static Kontent_MVC_Navigation.Configuration.Constants;

namespace Kontent_MVC_Navigation.Controllers
{
    [LocalizedRoute(EnglishCulture, "coffees")]
    [LocalizedRoute(SpanishCulture, "cafes")]
    public class CoffeesController : Controller
    {
        private readonly IDeliveryClient _deliveryClient;

        public CoffeesController(IDeliveryClient deliveryClient)
        {
            _deliveryClient = deliveryClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _deliveryClient.GetItemsAsync<Coffee>(
                new EqualsFilter("system.type", "coffee"),
                new EqualsFilter("system.language", CultureInfo.CurrentCulture.Name), // disable language fallback
                new LanguageParameter(CultureInfo.CurrentCulture.Name)
                );

            var coffees = response.Items;

            var coffeesContentResponse = await _deliveryClient.GetItemAsync<ListingPageContent>("coffees_listing_page",
                new LanguageParameter(CultureInfo.CurrentCulture.Name)
                );

            if (coffees.Count() > 0)
            {
                var coffeeListing = new ListingViewModel
                {
                    Content = coffeesContentResponse.Item,
                    RelatedItems = coffees
                };

                return View(coffeeListing);
            }
            else
            {
                return NotFound();
            }
        }

        [LocalizedRoute(EnglishCulture, "show")]
        [LocalizedRoute(SpanishCulture, "mostrar")]
        public async Task<IActionResult> Show(string url_pattern)
        {
            if (url_pattern != null)
            {
                var response = await _deliveryClient.GetItemsAsync<Coffee>(
                new EqualsFilter("elements.url_pattern", url_pattern),
                new LanguageParameter(CultureInfo.CurrentCulture.Name)
                );

                var coffee = response.Items.FirstOrDefault();

                return View(coffee);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}