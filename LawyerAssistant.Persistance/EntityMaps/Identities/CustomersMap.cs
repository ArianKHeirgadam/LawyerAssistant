using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Aggregates.Identities;

namespace LawyerAssistant.Persistance.EntityMaps.Identities;

public class CustomersMap : IEntityTypeConfiguration<CustomersModel>
{
    public void Configure(EntityTypeBuilder<CustomersModel> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);

        builder.Property(x => x.MobileNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.NationalCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.CreateDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CityId)
            .HasDefaultValue(null);

        builder.Property(x => x.ProvinceId)
            .HasDefaultValue(null);

        builder.HasOne(x => x.City)
            .WithMany(x => x.Customers)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Province)
            .WithMany(x => x.Customers)
            .HasForeignKey(x => x.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

