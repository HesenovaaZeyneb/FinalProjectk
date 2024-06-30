using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Evergreen_Persistence.Concretes
{
    public static class RegisterService
    {
        public static async void AddRoleServices(this IServiceProvider collection)
        {

            using var container = collection.CreateScope();
            var usermanager = container.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = container.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRole = await roleManager.RoleExistsAsync("Admin");

            if (!adminRole)
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            var adminUser = await usermanager.FindByNameAsync("Admin");

            if (adminUser is null)
            {
                var result = await usermanager.CreateAsync(new User
                {
                    UserName = "Admin",
                    Name = "Zeyneb",
                    Surname = "Hesenova",
                    Email = "zeynebhesenovaa2003@gmail.com",
                    EmailConfirmed = true,
                }, "1985zeynO!");

                if (result.Succeeded)
                {
                    var user = await usermanager.FindByNameAsync("Admin");
                    await usermanager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
