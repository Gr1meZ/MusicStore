using Microsoft.Extensions.DependencyInjection;

namespace MusicStore.WebApp.Services
{
    public static class AuthorizationServiceExtension
    {
        //this service allows to authenticate user via google, facebook and yandex 
        public static void AddServicesAuthentication(this IServiceCollection services)
        {
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
    }
}