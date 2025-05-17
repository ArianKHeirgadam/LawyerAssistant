using Domain.Aggregates.Identities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels.Enums;
using LawyerAssistant.Application.Extentions;

namespace LawyerAssistant.Persistance.EntityMaps.Identities;

public class UsersMap : IEntityTypeConfiguration<UsersEntity>
{
    public void Configure(EntityTypeBuilder<UsersEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(c => c.Id);
        builder.Property(x => x.UserName)
                  .IsRequired()
                  .HasMaxLength(50);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Gender)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.PicPath)
            .HasMaxLength(300);

        builder.Property(x => x.Role)
            .IsRequired();

        builder.HasData(new UsersEntity(
            1,
            "admin",
            "Admin",
            "User",
            "123456789".HashMD5(),
             true,
             "",
            UserRole.Admin
        ));
    }
}

