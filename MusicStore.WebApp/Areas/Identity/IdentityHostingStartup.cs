using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(MusicStore.WebApp.Areas.Identity.IdentityHostingStartup))]
namespace MusicStore.WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}