using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Persistance.EntityMaps.BasicDefinitionsModels;

public class BranchesMap : IEntityTypeConfiguration<BranchesModel>
{
    public void Configure(EntityTypeBuilder<BranchesModel> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(b => b.ComplexId)
            .IsRequired();

        builder.HasOne(b => b.Complexe)
            .WithMany(c => c.Branches)  
            .HasForeignKey(b => b.ComplexId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

