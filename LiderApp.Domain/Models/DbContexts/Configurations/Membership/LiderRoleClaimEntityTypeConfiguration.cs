using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiderApp.Domain.Models.DataContexts.Configurations.Membership
{
    public class LiderRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<LiderRoleClaim>
    {
        public void Configure(EntityTypeBuilder<LiderRoleClaim> builder)
        {
            builder.ToTable("RoleClaims", "Membership");
        }
    }
}
