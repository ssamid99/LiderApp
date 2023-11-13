using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiderApp.Domain.Models.DataContexts.Configurations.Membership
{
    public class LiderUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<LiderUserClaim>
    {
        public void Configure(EntityTypeBuilder<LiderUserClaim> builder)
        {
            builder.ToTable("UserClaims", "Membership");
        }
    }
}
