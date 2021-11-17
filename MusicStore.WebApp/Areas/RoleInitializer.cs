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
           
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //array of default roles
            string[] roleNames = { "Admin"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist) // if role is not exist
                {
                    //than create it
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here described default admin user
            var poweruser = new ApplicationUser
            {

                UserName = "admin@mail.ru",
                Email = "admin@mail.ru",
                EmailConfirmed = true
                
            };
            string userPWD = "Admin1*"; //admin's password
            var user = await userManager.FindByEmailAsync("admin@mail.ru");

            if(user == null)
            {
                var createPowerUser = await userManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
    }
}