using api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Database.EntityConfigs;

public class RoleConfig : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Roles");
        builder.Property(b => b.RowVersion);
        builder.HasOne<UserEntity>(s => s.CreatedByUserEntity)
            .WithMany()
            .HasForeignKey(r => r.CreatedBy);

        builder.HasOne<UserEntity>(s => s.UpdatedByUserEntity)
            .WithMany()
            .HasForeignKey(r => r.UpdatedBy);
    }
}