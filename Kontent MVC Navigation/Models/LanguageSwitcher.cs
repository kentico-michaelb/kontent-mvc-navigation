using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Kontent_MVC_Navigation.Models
{
    public class LanguageSwitcher
    {
        public CultureInfo CurrentUICulture { get; set; }
        public IEnumerable<LanguageSwitcherOption> CultureOptions { get; set; }
    }
}
