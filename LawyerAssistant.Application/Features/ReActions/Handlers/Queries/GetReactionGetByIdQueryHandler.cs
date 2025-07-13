using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.ReActions.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Features.ReActions.Handlers.Queries;

public class GetReactionGetByIdQueryHandler : IRequestHandler<GetReactionGetByIdQuery, SysResult<ReactionGetDTO>>
{
    private readonly IRepository<ReActionModel> _repository;
    private readonly IOptions<AppConfig> _options;
    public GetReactionGetByIdQueryHandler(IRepository<ReActionModel> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }

    public async Task<SysResult<ReactionGetDTO>> Handle(GetReactionGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(c => c.Id == request.Id).Include(c => c.Branch).Include(c => c.Files)
            .ThenInclude(c => c.Legal).Include(c => c.Files).ThenInclude(c => c.Customer).FirstOrDefaultAsync();


        return new SysResult<ReactionGetDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result != null ? new ReactionGetDTO
            {
                Id = result.Id,
                Customer = new GenericDTO() { Id = result.Files.IsLegal ? result.Files.Customer.Id : result.Files.Legal.Id, Title = result.Files.IsLegal ? result.Files.Customer.FirstName + " " + result.Files.Customer.LastName : result.Files.Legal.CompanyName },
                VisitDate = result.VisitDate.ToDateShortFormatString(_options),
                ActionType = result.ActionType != null ? new GenericDTO() { Id = result.ActionType.Id, Title = result.ActionType.Title } : null,
                Branch = result.Branch != null ? new GenericDTO() { Id = result.Branch.Id, Title = result.Branch.Title } : null,
                VisitTime = result.VisitTime.HasValue ? result.VisitTime.Value.ToTimePersianString() : null,
                IsCompleted = result.IsCompleted,
                GoingToBranch = result.GoingToBranch,
                TimeIsImportant = result.TimeIsImportant,
                IsLegal = result.Files.IsLegal
            } : null
        };
    }
}
