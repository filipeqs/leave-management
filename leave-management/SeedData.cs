using leave_management.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace leave_management
{
    public static class SeedData
    {
        public static async Task Seed(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers (userManager);
        }

        private static async Task SeedUsers(UserManager<Employee> userManager)
        {
            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                var user = new Employee { UserName = "admin@email.com", Email = "admin@email.com" };
                var result = await userManager.CreateAsync(user, "P@ssword1");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
        
        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var adminRoleExists = await roleManager.RoleExistsAsync("Administrator");
            if (!adminRoleExists)
            {
                var role = new IdentityRole { Name = "Administrator" };
                await roleManager.CreateAsync(role);
            }

            var employeeRoleExists = await roleManager.RoleExistsAsync("Employee");
            if (!employeeRoleExists)
            {
                var role = new IdentityRole { Name = "Employee" };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
