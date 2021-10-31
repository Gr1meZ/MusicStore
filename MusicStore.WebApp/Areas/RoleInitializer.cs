using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Data;

namespace MusicStore.WebApp.Areas
{
    public class RoleInitializer
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
           
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here you could create a super user who will maintain the web app
            var poweruser = new ApplicationUser
            {

                UserName = "admin@mail.ru",
                Email = "admin@mail.ru",
                EmailConfirmed = true
                
            };
            string userPWD = "Admin1*";
            var _user = await UserManager.FindByEmailAsync("admin@mail.ru");

            if(_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
    }
}