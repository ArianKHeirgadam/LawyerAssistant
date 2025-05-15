using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Persistance.ApplicationDbContexts;
using LawyerAssistant.Persistance.Contents;
using LawyerAssistant.Persistance.Repositories;

namespace Persistence.Repositories
{
    public class TransientRepository<T>  : BaseRepository<T> , ITransientRepository<T> where T : class
    {  
        public TransientRepository(IMainDBContext mainDBContext) : base()
        { 
            _db = (MainDBContext)mainDBContext;
            Entities = _db.Set<T>();
        }
    }
}