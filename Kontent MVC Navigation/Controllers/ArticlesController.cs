using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters.Filters;
using KenticoKontentModels;
using Kontent_MVC_Navigation.Models;

namespace Kontent_MVC_Navigation.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IDeliveryClient _deliveryClient;

        public ArticlesController(IDeliveryClient deliveryClient)
        {
            _deliveryClient = deliveryClient;
        }

        [Route("articles", Name = "articles")]
        public async Task<IActionResult> Index()
        {
            var articleResponse = await _deliveryClient.GetItemsAsync<Article>(
                new EqualsFilter("system.type", "article")
                );

            var articles = articleResponse.Items;

            var articlesContentResponse = await _deliveryClient.GetItemAsync<ListingPageContent>("articles_listing_page");

            if (articles.Count > 0)
            {
                var articleListing = new ListingViewModel
                {
                    Content = articlesContentResponse.Item,
                    RelatedItems = articleResponse.Items
                };

                return View(articleListing);
            }
            else
            {
                return NotFound();
            }

        }

        [Route("articles/{url_pattern}", Name = "show-article")]
        public async Task<IActionResult> Show(string url_pattern)
        {
            var response = await _deliveryClient.GetItemsAsync<Article>(
                new EqualsFilter("elements.url_pattern", url_pattern)
                );

            var article = response.Items.FirstOrDefault();

            return View(article);
        }
    }
}