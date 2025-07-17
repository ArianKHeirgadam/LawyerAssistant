using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Handlers;

public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, SysResult<GetBranchDTO>>
{
    private readonly IRepository<BranchesModel> _repository;

    public GetBranchByIdQueryHandler(IRepository<BranchesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<GetBranchDTO>> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branch = await _repository.Where( b => b.Id == request.Id).Include(b => b.Complexe).ThenInclude(c => c.City).ThenInclude(c => c.Province).FirstOrDefaultAsync();

        if (branch == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GetBranchDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new GetBranchDTO
            {
                Id = branch.Id,
                Title = branch.Title,
                Complex = branch.Complexe == null ? null : new GetComplexDTO
                {
                    Id = branch.Id,
                    Title = branch.Title,
                    City = branch.Complexe.City != null ? new GenericDTO() { Id = branch.Complexe.City.Id, Title = branch.Complexe.City.Name } : null,
                    Province = branch.Complexe.City != null ? new GenericDTO() { Id = branch.Complexe.City.Province.Id, Title = branch.Complexe.City.Province.Name } : null,
                }
            }
        };
    }
}