using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Persistance.EntityMaps.BasicDefinitionsModels;

internal class ComplexeMap : IEntityTypeConfiguration<ComplexesModel>
{
    public void Configure(EntityTypeBuilder<ComplexesModel> builder)
    {
        builder.ToTable("Complexes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(c => c.CityId)
            .IsRequired();

        builder.HasOne(c => c.City)
            .WithMany(c => c.Complexes) 
            .HasForeignKey(c => c.CityId)
            .OnDelete(DeleteBehavior.Restrict); 

    }
}