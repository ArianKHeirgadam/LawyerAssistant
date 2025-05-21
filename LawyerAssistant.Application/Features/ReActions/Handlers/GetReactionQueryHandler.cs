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
        var result = await _repository.Where(r => string.IsNullOrEmpty(request.CustomerName) ||
        (r.Customer.FirstName + " " + r.Customer.LastName).ToLower().Contains(request.CustomerName.ToLower()))
            .Include(r => r.Customer).Include(r => r.ActionType).Include(r => r.Branch)
            .Select(r => new ReactionGetDTO
            {
                CustomerId = r.CustomerId,
                CustomerName = r.Customer.FirstName + " " + r.Customer.LastName,
                ActionTypeId = r.ActionTypeId,
                ActionTypeTitle = r.ActionType.Title,
                Date = r.RegDateTime.ToDateShortFormatString(_options),
                Time = r.RegDateTime.ToString("HH:mm"),
                BranchId = r.BranchId,
                BranchName = r.Branch.Title,
                IsCompleted = r.IsCompleted,
            }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<ReactionGetDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}