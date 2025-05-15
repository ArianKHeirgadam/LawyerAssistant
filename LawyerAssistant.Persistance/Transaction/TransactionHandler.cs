using LawyerAssistant.Application.Contracts.Common;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Persistance.ApplicationDbContexts;
using System.Data;

namespace LawyerAssistant.Persistance.Transaction;

public class TransactionHandler : ITransactionHandler, IScoped
{
    private MainDBContext dbContext;
    public TransactionHandler(MainDBContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public async Task ExecuteAsync(IsolationLevel isolationLevel, Func<Task> action, Func<Task> errorHandler = null)
    {
        using (var transaction = await dbContext.Database.BeginTransactionAsync(CancellationToken.None))
        {
            try
            {
                await action.Invoke();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                if (errorHandler != null)
                    await errorHandler.Invoke();
                else
                    throw new Exception(ex.Message);
            }
        }
    }
}
