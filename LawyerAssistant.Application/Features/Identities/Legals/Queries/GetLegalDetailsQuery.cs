using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Identities.Legals.Queries;

public class GetLegalDetailsQuery : IRequest<SysResult<GetLegalCustomerDetailsDTO>>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
}
