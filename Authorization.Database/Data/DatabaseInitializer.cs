using System;
using System.Security.Claims;
using Authorization.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Database.Data
{
    public static class DatabaseInitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();
            
            var user = new ApplicationUser
            {
                UserName = "User",
                FirstName = "first name",
                LastName = "last name",
            };

            var result = userManager?.CreateAsync(user, "1234qwe").GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"))
                    .GetAwaiter().GetResult();
            }
        }
    }
}