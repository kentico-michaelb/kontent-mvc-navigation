using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontent_MVC_Navigation.Models
{
    public class ListingViewModel
    {
        public ListingPageContent Content { get; set; }
        public IEnumerable<object> RelatedItems { get; set; }
    }
}
