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
    public class BrewersController : Controller
    {
        private readonly IDeliveryClient _deliveryClient;

        public BrewersController(IDeliveryClient deliveryClient)
        {
            _deliveryClient = deliveryClient;
        }

        [Route("products/brewers", Name = "brewers")]
        public async Task<IActionResult> Index()
        {
            var brewersResponse = await _deliveryClient.GetItemsAsync<Brewer>(
                new EqualsFilter("system.type", "brewer")
                );

            var brewers = brewersResponse.Items;

            var brewersContentResponse = await _deliveryClient.GetItemAsync<ListingPageContent>("brewers_listing_page");

            if (brewers.Count > 0)
            {
                var brewersListing = new ListingViewModel
                {
                    Content = brewersContentResponse.Item,
                    RelatedItems = brewersResponse.Items
                };

                return View(brewersListing);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("products/brewers/{url_pattern}", Name = "show-brewer")]
        public async Task<IActionResult> Show(string url_pattern)
        {
            var response = await _deliveryClient.GetItemsAsync<Brewer>(
                new EqualsFilter("elements.url_pattern", url_pattern)
                );

            var brewer = response.Items.FirstOrDefault();

            return View(brewer);
        }
    }
}