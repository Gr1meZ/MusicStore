using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicStore.Data;
using MusicStore.Data.Data;
using MusicStore.Data.Interfaces;
using MusicStore.Data.Repositories;

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
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider )
        {
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}