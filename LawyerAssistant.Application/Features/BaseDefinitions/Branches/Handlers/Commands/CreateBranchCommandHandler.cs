using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Handlers.Commands;

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, SysResult<GetBranchDTO>>
{
    private readonly IRepository<BranchesModel> _repository;
    private readonly IRepository<ComplexesModel> _complexRepository;
    public CreateBranchCommandHandler(IRepository<BranchesModel> repository, IRepository<ComplexesModel> complexRepository)
    {
        _repository = repository;
        _complexRepository = complexRepository;
    }

    public async Task<SysResult<GetBranchDTO>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var complexe = await _complexRepository.FirstOrDefaultAsync(c => c.Id == request.ComplexId);

        if (complexe is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        var branch = new BranchesModel(request.Title, request.ComplexId);
        await _repository.AddAsync(branch);
        await _repository.SaveChangesAsync();

        return new SysResult<GetBranchDTO>
        {
            Value = new GetBranchDTO
            {
                Id = branch.Id,
                Title = branch.Title,
                ComplexId = branch.ComplexId,
                ComplexTitle = complexe.Title
            },
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}