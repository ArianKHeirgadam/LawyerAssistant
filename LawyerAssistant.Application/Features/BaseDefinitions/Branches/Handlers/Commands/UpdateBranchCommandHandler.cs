using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using System.Numerics;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Handlers.Commands;

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, SysResult<GetBranchDTO>>
{
    private readonly IRepository<BranchesModel> _repository;
    private readonly IRepository<ComplexesModel> _complexRepository;
    public UpdateBranchCommandHandler(IRepository<BranchesModel> repository, IRepository<ComplexesModel> complexRepository)
    {
        _repository = repository;
        _complexRepository = complexRepository;
    }


    public async Task<SysResult<GetBranchDTO>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await ValidateAndGetTheBranch(request);

        branch.Edit(request.Title, request.ComplexId);
        await _repository.SaveChangesAsync();

        return new SysResult<GetBranchDTO>
        {
            Value = new GetBranchDTO
            {
                Id = branch.Id,
                Title = branch.Title,
                Complex = branch.Complexe != null ? new GenericDTO() { Id = branch.Complexe.Id, Title = branch.Complexe.Title } : null
            },
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }


    private async Task<BranchesModel> ValidateAndGetTheBranch(UpdateBranchCommand request)
    {
        var branch = await _repository.FirstOrDefaultAsync(b => b.Id == request.Id);

        if (branch is null) throw new CustomException(SystemCommonMessage.BrachnIsNotFound);

        var complexe = await _complexRepository.FirstOrDefaultAsync(c => c.Id == request.ComplexId);

        if (complexe is null) throw new CustomException(SystemCommonMessage.ComplexeIsNotFound);

        return branch;
    }
}
