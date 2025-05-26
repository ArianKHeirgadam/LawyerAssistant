using LawyerAssistant.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Persistance.EntityMaps;

public class ReActionMap : IEntityTypeConfiguration<ReActionModel>
{
    public void Configure(EntityTypeBuilder<ReActionModel> builder)
    {
        builder.ToTable("ReActions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TimeIsImportant)
            .IsRequired();

        builder.Property(x => x.GoingToBranch)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .IsRequired(false);

        builder.Property(x => x.ComplexeId)
            .IsRequired(false);


        builder.Property(x => x.FileId)
            .IsRequired();

        builder.HasOne(x => x.Branch)
            .WithMany(c => c.Reactions)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Complexe)
            .WithMany(c => c.Reactions)
            .HasForeignKey(x => x.ComplexeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Files)
            .WithMany(c => c.Reactions)
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ActionType)
            .WithMany(c => c.Reactions)
            .HasForeignKey(x => x.ActionTypeId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(c => c.RegDateTime).HasColumnType("datetime").IsRequired();
        builder.Property(c => c.ModDateTime).HasColumnType("datetime");
    }
}
