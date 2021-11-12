using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MusicStore.Data;
using MusicStore.Data.Data;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Repositories;
using MusicStore.WebApp.Helpers;
using MusicStore.WebApp.Middlewares;
using MusicStore.WebApp.Resources;

namespace MusicStore.WebApp
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
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
             
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IItemTypeRepository, ItemTypeRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddAuthentication()
                .AddGoogle(options =>
                { 
                    options.ClientId = "938510314641-e898mt5jjciglv4825qidbns7toop53e.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "873904849961648";
                    options.ClientSecret = "75a4ba4ffac6df7bc120511264ad5ee3";
                })
                .AddYandex(options =>
                {
                    options.ClientId = "359e3e40f30d4a49a3dba035aaa1d437";
                    options.ClientSecret = "7c8f2e57af7a4ea6b35bfc16910d8fd3";
                });
                services.AddSession();
                services
                    .AddRazorPages()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
              
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider )
        {
            var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ru") };
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseRequestLocalization(app.ApplicationServices
                .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Cart}/{action=Items}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}