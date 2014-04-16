using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hopnscotch.Portal.Data
{
    public sealed class IdentityDbInitializer : CreateDatabaseIfNotExists<IdentityDbContext>
    {
        protected override void Seed(IdentityDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = "Administrators" };
            var userRole = new IdentityRole { Name = "RegisteredUsers" };

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);

            var administrator = new IdentityUser { UserName = "Administrator" };

            userManager.Create(administrator, "qweqwe");

            userManager.AddToRole(administrator.Id, "RegisteredUsers");
            userManager.AddToRole(administrator.Id, "Administrators");
        }
    }
}