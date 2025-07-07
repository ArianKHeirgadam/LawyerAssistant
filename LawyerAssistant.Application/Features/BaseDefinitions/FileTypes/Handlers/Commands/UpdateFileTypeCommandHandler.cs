using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class UpdateFileTypeCommandHandler : IRequestHandler<UpdateFileTypeCommand, SysResult<GenericDTO>>
{
    private readonly IRepository<FilesTypesModel> _repository;
    public UpdateFileTypeCommandHandler(IRepository<FilesTypesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<GenericDTO>> Handle(UpdateFileTypeCommand request, CancellationToken cancellationToken)
    {
        var fileType = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (fileType is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        fileType.Edit(request.Title);

        _repository.Update(fileType);
        await _repository.SaveChangesAsync();

        return new SysResult<GenericDTO>() { Value = new GenericDTO { Id = fileType.Id, Title = fileType.Name }, IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
