using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawyerAssistant.Persistance.EntityMaps.BasicDefinitionsModels;

public class ProvincesMap : IEntityTypeConfiguration<ProvincesModel>
{
    public void Configure(EntityTypeBuilder<ProvincesModel> entity)
    {
        entity.ToTable("Provinces");
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
        entity.Property(c => c.IsActive).HasColumnType("bit").IsRequired();
        entity.Property(c => c.Name).HasColumnType("nvarchar(50)").IsRequired();
        entity.HasMany(c => c.Cities).WithOne(c => c.Province).HasForeignKey(c => c.ProvinceId).OnDelete(DeleteBehavior.Restrict);

        //============================================================================ارتباطات
        //entity.HasOne(c => c.ModUser).WithMany().HasForeignKey(c => c.ModUserId).OnDelete(DeleteBehavior.Restrict);
        //entity.HasOne(c => c.Province).WithMany(c => c.Cities).HasForeignKey(c => c.ProvinceId).OnDelete(DeleteBehavior.Restrict);
    }
}
