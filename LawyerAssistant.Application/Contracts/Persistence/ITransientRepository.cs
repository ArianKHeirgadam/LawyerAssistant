namespace LawyerAssistant.Application.Contracts.Persistence;

public interface ITransientRepository<T> : IBaseRepository<T> where T : class
{
   
}