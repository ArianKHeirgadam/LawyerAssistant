using Domain.Aggregates.Identities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels.Enums;
using LawyerAssistant.Application.Extentions;

namespace LawyerAssistant.Persistance.EntityMaps.Identities;

public class UsersMap : IEntityTypeConfiguration<UsersModel>
{
    public void Configure(EntityTypeBuilder<UsersModel> builder)
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

        builder.HasData(new UsersModel(
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

