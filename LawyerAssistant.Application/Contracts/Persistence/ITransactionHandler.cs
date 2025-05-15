using System.Data;

namespace LawyerAssistant.Application.Contracts.Persistence;

public interface ITransactionHandler
{
    Task ExecuteAsync(IsolationLevel isolationLevel, Func<Task> action, Func<Task> errorHandler = null);
}