using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.FileTypes.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.Identities.Legals.Handlers;

public class GetLegalsListQueryHandler : IRequestHandler<GetLegalsListQuery, SysResult<List<GenericDTO>>>
{
    private readonly IRepository<LegalCustomersModel> _repository;
    public GetLegalsListQueryHandler(IRepository<LegalCustomersModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<List<GenericDTO>>> Handle(GetLegalsListQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(c => !string.IsNullOrWhiteSpace(request.Title) ? c.CompanyName.ToLower().Contains(request.Title.ToLower()) : true)
             .Select(c => new GenericDTO
             {
                 Title = c.CompanyName,
                 Id = c.Id,
             }).ToListAsync();


        return new SysResult<List<GenericDTO>>() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully, Value = result };
    }
}
