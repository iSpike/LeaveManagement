using LeaveManagement.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByNameAsync("admin@localhost.com").Result == null)
            {
                var user = new Employee()
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com"
                };

                var result = userManager.CreateAsync(user, "Pa$$w0rd").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole("Administrator");
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole("Employee");
                _ = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
