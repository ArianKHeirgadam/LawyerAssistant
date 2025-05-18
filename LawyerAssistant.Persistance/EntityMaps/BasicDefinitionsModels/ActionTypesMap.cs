using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Persistance.EntityMaps.BasicDefinitionsModels;

public class ActionTypesMap : IEntityTypeConfiguration<ActionTypesModel>
{
    public void Configure(EntityTypeBuilder<ActionTypesModel> builder)
    {
        builder.ToTable("ActionTypes");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(a => a.Priority)
            .IsRequired();

        builder.Property(a => a.RememberTime)
            .IsRequired();
    }
}