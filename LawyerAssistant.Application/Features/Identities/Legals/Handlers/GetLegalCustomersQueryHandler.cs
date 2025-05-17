using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.Identities.Legals.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Legals.Handlers;

public class GetLegalCustomersQueryHandler : IRequestHandler<GetLegalsQuery, SysResult<PagingResponse<GetLegalCustomersDTO>>>
{
    private readonly IRepository<LegalCustomersEntity> _repository;

    public GetLegalCustomersQueryHandler(IRepository<LegalCustomersEntity> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<PagingResponse<GetLegalCustomersDTO>>> Handle(GetLegalsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(c => string.IsNullOrEmpty(request.CompanyName) || c.CompanyName.Contains(request.CompanyName))
            .Select(c => new GetLegalCustomersDTO
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                LegalNationalCode = c.LegalNationalCode
            }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<GetLegalCustomersDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
