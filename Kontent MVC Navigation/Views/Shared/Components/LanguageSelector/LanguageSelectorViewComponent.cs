using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kontent_MVC_Navigation.Models;
using AspNetCore.Mvc.Routing.Localization;

using static Kontent_MVC_Navigation.Configuration.Constants;

namespace Kontent_MVC_Navigation.Views.Shared.Components.LanguageSelector
{
    public class LanguageSelectorViewComponent : ViewComponent
    {

        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        private readonly ILocalizedRoutingProvider _localizedRoutingProvider;

        public LanguageSelectorViewComponent(IOptions<RequestLocalizationOptions> localizationOptions, ILocalizedRoutingProvider localizedRoutingProvider)
        {
            _localizationOptions = localizationOptions;
            _localizedRoutingProvider = localizedRoutingProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUICulture = CultureInfo.CurrentUICulture;
            var currentController = HttpContext.Request.RouteValues["controller"].ToString();
            var currentAction = HttpContext.Request.RouteValues["action"].ToString();
            
            LocalizationDirection translationDirection = LocalizationDirection.TranslatedToOriginal;

            var cultureOptions = new List<LanguageSwitcherOption>();

            foreach (CultureInfo ci in _localizationOptions.Value.SupportedCultures)
            {
                if (ci.Name != DefaultCulture)
                {
                    translationDirection = LocalizationDirection.OriginalToTranslated;
                }

                var translation = await _localizedRoutingProvider.ProvideRouteAsync(
                    ci.Name,
                    currentController,
                    currentAction,
                    translationDirection);

                var option = new LanguageSwitcherOption {
                    DisplayName = ci.NativeName.Substring(0, ci.NativeName.IndexOf("(")),
                    CultureCode = ci.Name,
                    TranslatedController = translation.Controller,
                    TranslatedAction = translation.Action
                };

                cultureOptions.Add(option);
            }

            var languageSwitcher = new LanguageSwitcher
            {
                CurrentUICulture = currentUICulture,
                CultureOptions = cultureOptions
            };

            return View("languageSelector", languageSwitcher);
        }
    }
}
