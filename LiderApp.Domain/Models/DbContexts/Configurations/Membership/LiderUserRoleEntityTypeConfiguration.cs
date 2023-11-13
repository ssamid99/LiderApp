using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiderApp.Domain.Models.DataContexts.Configurations.Membership
{
    public class LiderUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<LiderUserRole>
    {
        public void Configure(EntityTypeBuilder<LiderUserRole> builder)
        {
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
