namespace PCartWeb.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PCartWeb.Models;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<PCartWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PCartWeb.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            string name = "PCartTeam@gmail.com";
            string password = "PcartTeam@2021";
            const string roleName = "Admin";
            var role1 = roleManager.FindByName(roleName);
            if (role1 == null)
            {
                role1 = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role1);
            }
            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = name,
                    Email = name,
                    EmailConfirmed = true
                };
                var result = userManager.Create(user, password);
                if (result.Succeeded)
                {
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }
            }

            var rolesForuser = userManager.GetRoles(user.Id);
            if (!rolesForuser.Contains(role1.Name))
            {
                var result = userManager.AddToRole(user.Id, role1.Name);
            }

            if (!roleManager.RoleExists("Coop Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Coop Admin";
                roleManager.Create(role);
            }

            // creating Creating Non-member role     
            if (!roleManager.RoleExists("Non-member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Non-member";
                roleManager.Create(role);
            }

            // creating Creating Member role     
            if (!roleManager.RoleExists("Member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Member";
                roleManager.Create(role);
            }

            // creating Creating Member role     
            if (!roleManager.RoleExists("Driver"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Driver";
                roleManager.Create(role);
            }
        }
    }
}