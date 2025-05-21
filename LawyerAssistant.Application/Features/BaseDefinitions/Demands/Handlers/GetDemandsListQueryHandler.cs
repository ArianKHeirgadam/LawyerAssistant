using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.Demands.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Demands.Handlers;

public class GetDemandsListQueryHandler : IRequestHandler<GetDemandsListQuery, SysResult<List<GenericDTO>>>
{
    private readonly IRepository<DemandsModel> _repository;
    public GetDemandsListQueryHandler(IRepository<DemandsModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<List<GenericDTO>>> Handle(GetDemandsListQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(c => !string.IsNullOrWhiteSpace(request.Title) ? c.Name.ToLower().Contains(request.Title.ToLower()) : true)
             .Select(c => new GenericDTO
             {
                 Title = c.Name,
                 Id = c.Id,
             }).ToListAsync();


        return new SysResult<List<GenericDTO>>() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully, Value = result };
    }
}
