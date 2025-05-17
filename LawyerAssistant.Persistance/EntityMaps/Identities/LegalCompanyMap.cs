using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;

namespace LawyerAssistant.Persistance.EntityMaps.Identities;

public class LegalCompanyMap : IEntityTypeConfiguration<LegalCustomersEntity>
{
    public void Configure(EntityTypeBuilder<LegalCustomersEntity> builder)
    {
        builder.ToTable("LegalCustomers");
        builder.HasKey(c => c.Id);

        builder.Property(x => x.CompanyName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LegalNationalCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.HasMany(x => x.CompanyCustomers)
               .WithOne(x => x.Legal)
               .HasForeignKey(x => x.LegalCompanyId) 
               .OnDelete(DeleteBehavior.Restrict);
    }
}
