namespace Astec.Data.Migrations
{
    using Astec.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Astec.Data.AstecDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Astec.Data.AstecDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            CreateConfigTitle(context);
            CreateUser(context);
            CreateApartmentSample(context);
        }
        private void CreateConfigTitle(AstecDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang Quản trị Admin Astec",

                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang Quản trị Admin Astec",

                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang Quản trị Admin Astec",

                });
            }
        }
        private void CreateUser(AstecDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AstecDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AstecDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "haolv@astec.vn",
                EmailConfirmed = true,
                BirthDate =Convert.ToDateTime("1996/04/26"),
                FullName = "Lê Vĩnh Hảo"

            };
            if (manager.Users.Count(x => x.UserName == "admin") == 0)
            {
                manager.Create(user, "123456");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("haolv@astec.vn");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }

        }
        private void CreateApartmentSample(AstecDbContext context)
        {
            if (context.Apartments.Count() == 0)
            {
                List<Apartment> listApartment = new List<Apartment>()
            {
                new Apartment() { ApartmentName="Căn hộ 1",Status=true },
                 
            };
                context.Apartments.AddRange(listApartment);
                context.SaveChanges();
            }

        }
    }
}
