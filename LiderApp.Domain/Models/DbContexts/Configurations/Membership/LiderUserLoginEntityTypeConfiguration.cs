using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiderApp.Domain.Models.DataContexts.Configurations.Membership
{
    public class LiderUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<LiderUserLogin>
    {
        public void Configure(EntityTypeBuilder<LiderUserLogin> builder)
        {
            builder.ToTable("UserLogins", "Membership");
        }
    }
}
