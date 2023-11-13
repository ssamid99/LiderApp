using LiderApp.Domain.Models.Entity;
using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiderApp.Domain.Models.DbContexts
{
    public partial class LiderAppDbContext : IdentityDbContext<LiderUser,LiderRole,string,LiderUserClaim,LiderUserRole,LiderUserLogin,LiderRoleClaim,LiderUserToken>
    {
        public LiderAppDbContext(DbContextOptions<LiderAppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Menu> Menus { get; set; }
        [NotMapped]
        public DbSet<SliderImages> SliderImages { get; set; }
        public DbSet<RepairOrder> RepairOrders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<WebImage> WebImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(LiderAppDbContext).Assembly);
        }
    }
}
