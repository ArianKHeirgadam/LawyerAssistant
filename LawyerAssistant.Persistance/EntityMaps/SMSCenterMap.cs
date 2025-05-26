using LawyerAssistant.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Persistance.EntityMaps;

public class SMSCenterMap : IEntityTypeConfiguration<SMSCenterModel>
{
    public void Configure(EntityTypeBuilder<SMSCenterModel> builder)
    {
        builder.ToTable("ReActions");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Reaction)
            .WithMany(c => c.SMSCenters)
            .HasForeignKey(x => x.ReactionId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(c => c.RegDateTime).HasColumnType("datetime").IsRequired();
        builder.Property(c => c.ModDateTime).HasColumnType("datetime");
    }
}
