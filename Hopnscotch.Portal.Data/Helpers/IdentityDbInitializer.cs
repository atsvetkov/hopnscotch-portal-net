using System.Data.Entity;
using Hopnscotch.Portal.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hopnscotch.Portal.Data
{
    public sealed class IdentityDbInitializer : DropCreateDatabaseAlways<IdentityDbContext>
    {
        protected override void Seed(IdentityDbContext context)
        {
            CreateRoles(context, "Administrators", "RegisteredUsers", "Teachers", "Managers", "Students");

            CreateUsers(context);
        }

        private static void CreateUsers(IdentityDbContext context)
        {
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);
            userManager.UserValidator = new UserValidator<IdentityUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
            // userManager.PasswordValidator = new MinimumLengthValidator(3);

            var administrator = new IdentityUser {UserName = "Administrator"};
            userManager.Create(administrator, "qwe");
            userManager.AddToRole(administrator.Id, "RegisteredUsers");
            userManager.AddToRole(administrator.Id, "Administrators");
        }

        private static void CreateRoles(IdentityDbContext context, params string[] roles)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (var roleName in roles)
            {
                roleManager.Create(new IdentityRole { Name = roleName });
            }
        }
    }
}