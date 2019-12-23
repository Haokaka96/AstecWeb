using Astec.Model.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Astec.Data
{
    public class AstecDbContext : IdentityDbContext<ApplicationUser>
    {
        public AstecDbContext() : base("name=AstecConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Wards> Wardses { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<ResidentImage> ResidentImages { get; set; }
        public DbSet<FingerPrint> FingerPrints { get; set; }
        public DbSet<ApplicationModule> ApplicationModules { get; set; }

        public static AstecDbContext Create()
        {
            return new AstecDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}