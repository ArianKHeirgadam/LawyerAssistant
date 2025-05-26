using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.ReActions.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Features.ReActions.Handlers;
public class GetReactionQueryHandler : IRequestHandler<GetReactionQuery, SysResult<PagingResponse<ReactionGetDTO>>>
{
    private readonly IRepository<ReActionModel> _repository;
    private readonly IOptions<AppConfig> _options;
    public GetReactionQueryHandler(IRepository<ReActionModel> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }

    public async Task<SysResult<PagingResponse<ReactionGetDTO>>> Handle(GetReactionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository
            .Where(c => c.IsCompleted == request.IsCompleted )
            .Where(c => request.To != null && request.To.HasValue ? c.VisitDate <= request.To.Value  : true)
            .Where(c => request.From != null && request.From.HasValue ? c.VisitDate >= request.From.Value : true)
            .Include(c => c.Branch).Include(c => c.Files).ThenInclude(c => c.Legal).
            Include(c => c.Files).ThenInclude(c => c.Customer)
            .Select(r => new ReactionGetDTO
            {
                Id = r.Id,
                CustomerId = r.Files.IsLegal ? r.Files.Customer.Id : r.Files.Legal.Id,
                ActionTypeId = r.ActionTypeId,
                BranchId = r.BranchId,
                CustomerName = r.Files.IsLegal ? r.Files.Customer.FirstName + " " + r.Files.Customer.LastName : r.Files.Legal.CompanyName,
                ActionTypeTitle = r.ActionType.Title,
                VisitDate = r.VisitDate.ToDateShortFormatString(_options),
                VisitTime = r.VisitTime.HasValue ? r.VisitTime.Value.ToTimePersianString() : null,
                BranchName = r.Branch.Title,

                IsCompleted = r.IsCompleted,
                GoingToBranch = r.GoingToBranch,
                TimeIsImportant = r.TimeIsImportant,
                IsLegal = r.Files.IsLegal

            }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<ReactionGetDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}