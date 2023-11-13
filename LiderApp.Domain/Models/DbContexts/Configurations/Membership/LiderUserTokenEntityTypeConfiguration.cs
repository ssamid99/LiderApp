using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiderApp.Domain.Models.DataContexts.Configurations.Membership
{
    public class LiderUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<LiderUserToken>
    {
        public void Configure(EntityTypeBuilder<LiderUserToken> builder)
        {
            builder.ToTable("UserTokens", "Membership");
        }
    }
}
