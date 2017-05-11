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

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // Initialize default user
            if (!context.Users.Any(u => u.Email == "manager@gmail.com"))
            {
                var user1ToInsert = new ApplicationUser { Email = "manager@gmail.com", UserName = "manager@gmail.com", PhoneNumber = "0123456789", AvatarUrl = "default.png", Firstname = "Manager" };
                userManager.Create(user1ToInsert, "1Manager!");
                userManager.AddToRole(user1ToInsert.Id, RoleTypes.Manager.Get());
            }

            if (!context.Users.Any(u => u.Email == "moderator@gmail.com"))
            {
                var user2ToInsert = new ApplicationUser { Email = "moderator@gmail.com", UserName = "moderator@gmail.com", PhoneNumber = "0123456789", AvatarUrl = "default.png", Firstname = "Moderator" };
                userManager.Create(user2ToInsert, "1Moderator!");
                userManager.AddToRole(user2ToInsert.Id, RoleTypes.Moderator.Get());
            }
        }
    }
}
