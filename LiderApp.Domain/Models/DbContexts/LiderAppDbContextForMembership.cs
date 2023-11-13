using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiderApp.Domain.Models.DbContexts
{
    public partial class LiderAppDbContext
    {
        [NotMapped]
        public DbSet<LiderRole> LiderRoles { get; set; }
        [NotMapped]
        public DbSet<LiderRoleClaim> LiderRoleClaims { get; set; }
        [NotMapped]
        public DbSet<LiderUser> LiderUsers { get; set; }
        [NotMapped]
        public DbSet<LiderUserClaim> LiderUserClaims { get; set; }
        [NotMapped]
        public DbSet<LiderUserLogin> LiderUserLogins { get; set; }
        [NotMapped]
        public DbSet<LiderUserRole> LiderUserRoles { get; set; }
        [NotMapped]
        public DbSet<LiderUserToken> LiderUserTokens { get; set; }
    }
}
