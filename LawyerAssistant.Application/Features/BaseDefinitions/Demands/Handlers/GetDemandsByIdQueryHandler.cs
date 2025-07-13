using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers;

public class GetDemandsByIdQueryHandler : IRequestHandler<GetDemandsByIdQuery, SysResult<GetDemandsDTO>>
{
    private readonly IRepository<DemandsModel> _repository;
    public GetDemandsByIdQueryHandler(IRepository<DemandsModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<GetDemandsDTO>> Handle(GetDemandsByIdQuery request, CancellationToken cancellationToken)
    {
        var demands = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id, c => c.FilesType);

        if (demands is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GetDemandsDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new GetDemandsDTO
            {
                Id = demands.Id,
                Title = demands.Name,
                FileType = demands.FilesType != null ? new GenericDTO() { Id = demands.FilesType.Id, Title = demands.FilesType.Name } : null
            }
        };
    }
}
