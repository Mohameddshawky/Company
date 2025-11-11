using Company.DAL.Data.Contexts;
using Company.DAL.Models.Identitymodule;
using Microsoft.AspNetCore.Identity;

namespace Company.PL.DataSeed
{
    public class DataSeeding(
        CompanyDbContext companyDbContext
        ,UserManager<AppUser> userManager
        ,RoleManager<IdentityRole> roleManager
        )
    {


        public async Task SeedIdentityDataAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (!userManager.Users.Any())
                {
                    AppUser Admin = new()
                    {
                        Firstname="Mohamed",
                        Lastname="Shawky",                       
                        UserName = "MohamedShawky",
                        Email = "Shawky1mohamed2@gmail.com",
                        PhoneNumber = "01113560216"
                    };
                 
                    await userManager.CreateAsync(Admin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(Admin, "Admin");
                }
            }
            catch (Exception ex)
            {
                //handle ex
            }
        }
    }
}
