using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Handlers;

public class GetComplexByIdQueryHandler : IRequestHandler<GetComplexByIdQuery, SysResult<GetComplexDTO>>
{
    private readonly IRepository<ComplexesModel> _repository;

    public GetComplexByIdQueryHandler(IRepository<ComplexesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<GetComplexDTO>> Handle(GetComplexByIdQuery request, CancellationToken cancellationToken)
    {
        var complex = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id, c => c.City);

        if (complex == null)
            throw new CustomException(SystemCommonMessage.DataWasNotFound);

        var dto = new GetComplexDTO
        {
            Id = complex.Id,
            Title = complex.Title,
            CityId = complex.CityId,
            CityTitle = complex.City?.Name ?? string.Empty
        };

        return new SysResult<GetComplexDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = dto
        };
    }
}