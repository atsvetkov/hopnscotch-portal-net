using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hopnscotch.Portal.Data
{
    public sealed class IdentityDbInitializer : CreateDatabaseIfNotExists<IdentityDbContext>
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
            userManager.PasswordValidator = new MinimumLengthValidator(3);

            var admin = new IdentityUser {UserName = "Admin"};
            userManager.Create(admin, "qwe");
            userManager.AddToRole(admin.Id, "RegisteredUsers");
            userManager.AddToRole(admin.Id, "Administrators");

            var guzel = new IdentityUser {UserName = "takutdinova.g@gmail.com"};
            userManager.Create(guzel, "qwe");
            userManager.AddToRole(guzel.Id, "RegisteredUsers");
            userManager.AddToRole(guzel.Id, "Teachers");

            var manager = new IdentityUser { UserName = "iammamba@gmail.com" };
            userManager.Create(manager, "qwe");
            userManager.AddToRole(manager.Id, "RegisteredUsers");
            userManager.AddToRole(manager.Id, "Managers");

            var student = new IdentityUser { UserName = "Student" };
            userManager.Create(student, "qwe");
            userManager.AddToRole(student.Id, "RegisteredUsers");
            userManager.AddToRole(student.Id, "Students");
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