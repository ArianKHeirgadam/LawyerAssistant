using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class CreateFileTypeCommandHandler : IRequestHandler<CreateFileTypeCommand, SysResult<GenericDTO>>
{
    private readonly IRepository<FilesTypesModel> _repository;
    public CreateFileTypeCommandHandler(IRepository<FilesTypesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<GenericDTO>> Handle(CreateFileTypeCommand model, CancellationToken cancellationToken)
    {
        var fileType = new FilesTypesModel(model.Title);

        await _repository.AddAsync(fileType);
        await _repository.SaveChangesAsync();

        return new SysResult<GenericDTO>() { Value= new GenericDTO { Id = fileType.Id , Title = fileType.Name} , IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
