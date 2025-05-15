using LawyerAssistant.Domain.Base.Contracts;
using LawyerAssistant.Persistance.Contents;
using Microsoft.EntityFrameworkCore;
using Persistence.Extentions;
using System.Reflection;

namespace LawyerAssistant.Persistance.ApplicationDbContexts;

public class MainDBContext : DbContext, IMainDBContext
{

    public MainDBContext(DbContextOptions<MainDBContext> options)
       : base(options)
    {
    }

    //*************************************************************** Model Creating Start
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var entitiesAssembly = typeof(IEntity).Assembly;
        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
    }
    //*************************************************************** Model Creating End
}
