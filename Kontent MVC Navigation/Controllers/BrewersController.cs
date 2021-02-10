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
    [LocalizedRoute(EnglishCulture, "brewers")]
    [LocalizedRoute(SpanishCulture, "cafeteras")]
    public class BrewersController : Controller
    {
        private readonly IDeliveryClient _deliveryClient;

        public BrewersController(IDeliveryClient deliveryClient)
        {
            _deliveryClient = deliveryClient;
        }
        public async Task<IActionResult> Index()
        {
            var brewersResponse = await _deliveryClient.GetItemsAsync<Brewer>(
                new EqualsFilter("system.type", "brewer"),
                new EqualsFilter("system.language", CultureInfo.CurrentCulture.Name), // disable language fallback
                new LanguageParameter(CultureInfo.CurrentCulture.Name)
                );

            var brewers = brewersResponse.Items;

            var brewersContentResponse = await _deliveryClient.GetItemAsync<ListingPageContent>("brewers_listing_page",
                new LanguageParameter(CultureInfo.CurrentCulture.Name)
                );

            if (brewers.Count() > 0)
            {
                var brewerListing = new ListingViewModel
                {
                    Content = brewersContentResponse.Item,
                    RelatedItems = brewers
                };

                return View(brewerListing);
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
                var response = await _deliveryClient.GetItemsAsync<Brewer>(
                new EqualsFilter("elements.url_pattern", url_pattern),
                new LanguageParameter(CultureInfo.CurrentCulture.Name)
                );

                var brewer = response.Items.FirstOrDefault();

                return View(brewer);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}