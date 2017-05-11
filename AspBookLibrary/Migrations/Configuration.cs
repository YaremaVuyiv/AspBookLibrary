using System.Collections.Generic;
using System.Linq;
using AspBookLibrary.Extensions;
using AspBookLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspBookLibrary.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AspBookLibrary.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Initialize default identity roles
            var storeR = new RoleStore<IdentityRole>(context);
            var managerR = new RoleManager<IdentityRole>(storeR);
            List<IdentityRole> identityRoles = new List<IdentityRole>
            {
                new IdentityRole() { Name = RoleTypes.Member.Get() },
                new IdentityRole() { Name = RoleTypes.Moderator.Get() },
                new IdentityRole() { Name = RoleTypes.Manager.Get() }
            };
            foreach (IdentityRole role in identityRoles)
            {
                if (!(context.Roles.Any(r => r.Name == role.Name)))
                {
                    managerR.Create(role);
                }
            }

            // Initialize default user
            if (!(context.Users.Any(u => u.UserName == "admin@gmail.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "admin@gmail.com", PhoneNumber = "0123456789", AvatarUrl = "default.png", Firstname = "Manager"};
                userManager.Create(userToInsert, "1Admin!");
                userManager.AddToRole(userToInsert.Id, RoleTypes.Manager.Get());
            }
        }
    }
}
