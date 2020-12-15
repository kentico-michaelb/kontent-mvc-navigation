using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Localization;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Extensions;
using KenticoKontentModels;
using Kontent_MVC_Navigation.Infrastructure;
using Kentico.AspNetCore.LocalizedRouting.Extensions;
using Kentico.AspNetCore.LocalizedRouting;

namespace Kontent_MVC_Navigation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable configuration services
            services.AddOptions();

            services.AddLocalizedRouting();
            services.AddControllersWithViews();

            // README from localization package suggests that Customized provider is not necessary for basic routing
            //services.AddSingleton<ILocalizedRoutingProvider, CustomLocalizedRouteProvider>();

            services.AddSingleton<CustomLocalizedRoutingTranslationTransformer>();

            services.AddLocalization();

            // Kontent services
            services.AddSingleton<ITypeProvider, CustomTypeProvider>()
                    .AddDeliveryClient(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseExceptionHandler("/error/500");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            var supportedCultures = new[]
{
                    new CultureInfo("es-ES"),
                    new CultureInfo("en-US"),
                };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapDynamicControllerRoute<CustomLocalizedRoutingTranslationTransformer>("{culture}/{controller}/{action}/{id?}");
                endpoints.MapControllerRoute("default", "{culture=en-US}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
