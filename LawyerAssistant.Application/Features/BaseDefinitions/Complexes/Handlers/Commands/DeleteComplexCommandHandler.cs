using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Handlers.Commands;

public class DeleteComplexCommandHandler : IRequestHandler<DeleteComplexCommand, SysResult>
{
    private readonly IRepository<ComplexesModel> _repository;

    public DeleteComplexCommandHandler(IRepository<ComplexesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(DeleteComplexCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var actions = await _repository
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync();

            if (actions.Count != request.Ids.Count)
                throw new CustomException(SystemCommonMessage.DataWasNotFound);

            _repository.DeleteRange(actions);
            await _repository.SaveChangesAsync();

            return new SysResult
            {
                IsSuccess = true,
                Message = SystemCommonMessage.OperationDoneSuccessfully
            };

        }
        catch (Exception ex)
        {
            return new SysResult
            {
                IsSuccess = false,
                Message = SystemCommonMessage.CantRemoveBecauseThereIsDependy
            };
        }
    }
}
