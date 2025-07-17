using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var branch = await _repository.FirstOrDefaultAsync(b => b.Id == request.Id);

        if (branch is null) throw new CustomException(SystemCommonMessage.BrachnIsNotFound);

        var complexe = await ValidateAndGetTheBranch(request);

        branch.Edit(request.Title, request.ComplexId);
        await _repository.SaveChangesAsync();

        return new SysResult<GetBranchDTO>
        {
            Value = new GetBranchDTO
            {
                Id = branch.Id,
                Title = branch.Title,
                Complex = branch.Complexe == null ? null : new GetComplexDTO
                {
                    Id = branch.Id,
                    Title = branch.Title,
                    City = complexe.City != null ? new GenericDTO() { Id = complexe.City.Id, Title = complexe.City.Name } : null,
                    Province = complexe.City != null ? new GenericDTO() { Id = complexe.City.Province.Id, Title = complexe.City.Province.Name } : null,
                }
            },
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }


    private async Task<ComplexesModel> ValidateAndGetTheBranch(UpdateBranchCommand request)
    {
        var complexe = await _complexRepository.Where(c => c.Id == request.ComplexId).Include(c => c.City).ThenInclude(c => c.Province).FirstOrDefaultAsync();

        if (complexe is null) throw new CustomException(SystemCommonMessage.ComplexeIsNotFound);

        return complexe;
    }
}
