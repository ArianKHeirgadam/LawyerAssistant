using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.IO;

namespace LawyerAssistant.Persistance.EntityMaps.BasicDefinitionsModels;

public class CitiesMap : IEntityTypeConfiguration<CitiesModel>
{
    public void Configure(EntityTypeBuilder<CitiesModel> entity)
    {
        entity.ToTable("Cities");
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id).HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
        entity.Property(c => c.IsActive).HasColumnType("bit").IsRequired();
        entity.Property(c => c.Name).HasColumnType("nvarchar(50)").IsRequired();
        entity.Property(c => c.RegDateTime).HasColumnType("datetime").IsRequired();
        entity.Property(c => c.ModDateTime).HasColumnType("datetime");
        //============================================================================ارتباطات
        //entity.HasOne(c => c.ModUser).WithMany().HasForeignKey(c => c.ModUserId).OnDelete(DeleteBehavior.Restrict);
        //entity.HasOne(c => c.Province).WithMany(c => c.Cities).HasForeignKey(c => c.ProvinceId).OnDelete(DeleteBehavior.Restrict);

    }

    private static object[] Seed()
    {
        string json = File.ReadAllText("./SeedData/cities.json");

        List<CityProvider> cities = JsonConvert.DeserializeObject<List<CityProvider>>(json);

        return cities.ConvertAll(x => new
        {
            x.Id,
            x.Name,
            x.Code,
            x.ProvinceId
        }).ToArray();
    }
}
