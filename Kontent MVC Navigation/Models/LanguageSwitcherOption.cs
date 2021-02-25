using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontent_MVC_Navigation.Models
{
    public class LanguageSwitcherOption
    {
        public string DisplayName { get; set; }

        public string CultureCode { get; set; }

        public string TranslatedController { get; set; }

        public string TranslatedAction { get; set; }
        public string UrlPattern { get; set; }
    }
}
