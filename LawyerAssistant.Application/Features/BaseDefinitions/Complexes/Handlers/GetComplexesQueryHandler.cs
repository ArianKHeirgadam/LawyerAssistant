using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Handlers;

public class GetComplexesQueryHandler : IRequestHandler<GetComplexesQuery, SysResult<PagingResponse<GetComplexDTO>>>
{
    private readonly IRepository<ComplexesModel> _repository;

    public GetComplexesQueryHandler(IRepository<ComplexesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<PagingResponse<GetComplexDTO>>> Handle(GetComplexesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(c =>string.IsNullOrEmpty(request.Title) || c.Title.Contains(request.Title))
            .Include(c => c.City).Select(c => new GetComplexDTO
            {
                Id = c.Id,
                Title = c.Title,
                City = c.City != null ? new GenericDTO() { Id = c.City.Id, Title = c.City.Name } : null
            }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<GetComplexDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
