using Microsoft.AspNetCore.Identity;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Database
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(BusinessLogic.Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(BusinessLogic.Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(BusinessLogic.Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(BusinessLogic.Enums.Roles.Basic.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "lukasz.gulczynski90@gmail.com",
                FirstName = "Łukasz",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, BusinessLogic.Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, BusinessLogic.Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, BusinessLogic.Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, BusinessLogic.Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
