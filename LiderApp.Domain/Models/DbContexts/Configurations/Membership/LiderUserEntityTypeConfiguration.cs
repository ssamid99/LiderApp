using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiderApp.Domain.Models.DataContexts.Configurations.Membership
{
    public class LiderUserEntityTypeConfiguration : IEntityTypeConfiguration<LiderUser>
    {
        public void Configure(EntityTypeBuilder<LiderUser> builder)
        {
            builder.ToTable("Users", "Membership");
        }
    }
}
