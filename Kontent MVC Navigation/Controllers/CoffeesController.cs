using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters.Filters;
using KenticoKontentModels;
using Kontent_MVC_Navigation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kontent_MVC_Navigation.Controllers
{
    public class CoffeesController : Controller
    {
        private readonly IDeliveryClient _deliveryClient;

        public CoffeesController(IDeliveryClient deliveryClient)
        {
            _deliveryClient = deliveryClient;
        }

        [Route("products/coffees", Name = "coffees")]
        public async Task<IActionResult> Index()
        {
            var response = await _deliveryClient.GetItemsAsync<Coffee>(
                new EqualsFilter("system.type", "coffee")
                );

            var coffees = response.Items;

            var coffeesContentResponse = await _deliveryClient.GetItemAsync<ListingPageContent>("coffees_listing_page");

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

        [Route("products/coffees/{url_pattern}", Name = "show-coffee")]
        public async Task<IActionResult> Show(string url_pattern)
        {
            var response = await _deliveryClient.GetItemsAsync<Coffee>(
                new EqualsFilter("elements.url_pattern", url_pattern)
                );

            var coffee = response.Items.FirstOrDefault();

            return View(coffee);
        }
    }
}