using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Numerics;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Handlers;
public class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, SysResult<PagingResponse<GetBranchDTO>>>
{
    private readonly IRepository<BranchesModel> _repository;

    public GetBranchesQueryHandler(IRepository<BranchesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<PagingResponse<GetBranchDTO>>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
    {

        var result = await _repository.Where(c => !string.IsNullOrWhiteSpace(request.Title) ? c.Title.Contains(request.Title)  : true)
            .Include(b => b.Complexe).ThenInclude( c=> c.City).ThenInclude(c => c.Province)
            .Where(c => !string.IsNullOrWhiteSpace(request.Complex) ? c.Complexe.Title.Contains(request.Complex) : true)
            .Select(b => new GetBranchDTO
            
        {
                Id = b.Id,
                Title = b.Title,
                Complex = b.Complexe == null ? null : new GetComplexDTO
                {
                    Id = b.Complexe.Id,
                    Title = b.Complexe.Title,
                    City = b.Complexe.City != null ? new GenericDTO() { Id = b.Complexe.City.Id, Title = b.Complexe.City.Name } : null,
                    Province = b.Complexe.City != null ? new GenericDTO() { Id = b.Complexe.City.Province.Id, Title = b.Complexe.City.Province.Name } : null,
                }
        }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<GetBranchDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}