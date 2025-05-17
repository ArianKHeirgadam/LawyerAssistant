using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Handlers;

public class GetProvinceByIdQueryHandler : IRequestHandler<GetProvinceByIdQuery, SysResult<GenericDTO>>
{
    private readonly IRepository<ProvincesModel> _repository;
    public GetProvinceByIdQueryHandler(IRepository<ProvincesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<GenericDTO>> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        var province = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (province is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GenericDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new GenericDTO
            {
                Id = province.Id,
                Title = province.Name,
            }
        };
    }
}
