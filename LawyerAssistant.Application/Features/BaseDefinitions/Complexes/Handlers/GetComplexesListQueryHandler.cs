using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Handlers;

public class GetComplexesListQueryHandler : IRequestHandler<GetComplexesListQuery, SysResult<List<GenericDTO>>>
{
    private readonly IRepository<ComplexesModel> _repository;
    public GetComplexesListQueryHandler(IRepository<ComplexesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<List<GenericDTO>>> Handle(GetComplexesListQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(c => !string.IsNullOrWhiteSpace(request.Title) ? c.Title.ToLower().Contains(request.Title.ToLower()) : true)
             .Select(c => new GenericDTO
             {
                 Title = c.Title,
                 Id = c.Id,
             }).ToListAsync();


        return new SysResult<List<GenericDTO>>() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully, Value = result };
    }
}
