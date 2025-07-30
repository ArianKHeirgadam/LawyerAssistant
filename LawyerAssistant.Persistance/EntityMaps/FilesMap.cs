using LawyerAssistant.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Persistance.EntityMaps;

public class FilesMap : IEntityTypeConfiguration<FilesModel>
{
    public void Configure(EntityTypeBuilder<FilesModel> builder)
    {
        builder.ToTable("Files");

        builder.HasKey(x => x.Id);

        builder.Property(f => f.IsLegal)
               .HasDefaultValue(false);

        builder.HasOne(f => f.Customer)
               .WithMany(f => f.Files)
               .HasForeignKey(f => f.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Legal)
               .WithMany(f => f.Files)
               .HasForeignKey(f => f.LegalId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Demand)
               .WithMany(f => f.Files)
               .HasForeignKey(f => f.DemandId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.RegDateTime).HasColumnType("datetime").IsRequired();
        builder.Property(c => c.ModDateTime).HasColumnType("datetime");
    }
}
