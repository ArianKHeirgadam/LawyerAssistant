using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

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
        var complex = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (complex == null)
            throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(complex);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}
