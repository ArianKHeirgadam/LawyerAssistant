using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Handlers.Commands;

public class CreateActionTypesCommandHandler : IRequestHandler<CreateActionTypesCommand, SysResult<ActionDto>>
{
    private readonly IRepository<ActionTypesModel> _repository;
    private readonly ISender _sender;
    public CreateActionTypesCommandHandler(IRepository<ActionTypesModel> repository, ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }

    public async Task<SysResult<ActionDto>> Handle(CreateActionTypesCommand request, CancellationToken cancellationToken)
    {
        var action = new ActionTypesModel(request.Title, request.Priority);
        await _repository.AddAsync(action);
        await _repository.SaveChangesAsync();
        return new SysResult<ActionDto>() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully , Value = new ActionDto { 
            Id = action.Id, 
            Priority = action.Priority,
            Title = action.Title
        } };
    }
}

