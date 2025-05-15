using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Persistance.ApplicationDbContexts;
using LawyerAssistant.Persistance.Repositories;

namespace Persistence.Repositories
{
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : class
    {
        //********************************************************************************************************************
        public Repository(MainDBContext dbContext) : base()
        {
             _db = dbContext;
            Entities = _db.Set<T>();
        }
        //********************************************************************************************************************

    }
}