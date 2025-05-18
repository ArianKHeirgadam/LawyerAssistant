using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers;

public class GetFileTypeByIdQueryHandler : IRequestHandler<GetFileTypeByIdQuery, SysResult<GenericDTO>>
{
    private readonly IRepository<FilesTypesModel> _repository;
    public GetFileTypeByIdQueryHandler(IRepository<FilesTypesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<GenericDTO>> Handle(GetFileTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var fileType = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (fileType is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GenericDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new GenericDTO
            {
                Id = fileType.Id,
                Title = fileType.Name
            }
        };
    }
}
