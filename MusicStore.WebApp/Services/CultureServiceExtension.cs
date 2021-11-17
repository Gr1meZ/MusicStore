using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace MusicStore.WebApp.Services
{
    public static class CultureServiceExtension
    {
        //this service allows to manage language of application
        public static void AddCulture(this IServiceCollection services)
        {
            //set default folder for resources dictionaries
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            //localize data annotations 
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
             
            services.Configure<RequestLocalizationOptions>(options =>
            {
                //supported languages
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }
    }
}