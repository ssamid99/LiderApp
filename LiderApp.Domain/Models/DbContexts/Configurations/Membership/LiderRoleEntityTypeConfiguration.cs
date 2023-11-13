using LiderApp.Domain.Models.Entity.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiderApp.Domain.Models.DataContexts.Configurations.Membership
{
    public class LiderRoleEntityTypeConfiguration : IEntityTypeConfiguration<LiderRole>
    {
        public void Configure(EntityTypeBuilder<LiderRole> builder)
        {
            builder.ToTable("Roles", "Membership");
        }
    }
}
