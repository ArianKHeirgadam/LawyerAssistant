using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

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
        var branch = await _repository.FirstOrDefaultAsync( b => b.Id == request.Id, b => b.Complexe );

        if (branch == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GetBranchDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new GetBranchDTO
            {
                Id = branch.Id,
                Title = branch.Title,
                ComplexId = branch.ComplexId,
                ComplexTitle = branch.Complexe?.Title ?? string.Empty
            }
        };
    }
}