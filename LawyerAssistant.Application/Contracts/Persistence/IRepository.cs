

namespace LawyerAssistant.Application.Contracts.Persistence;

public interface IRepository<T> : IBaseRepository<T> where T : class
{
    object Include(Func<object, object> value);
}