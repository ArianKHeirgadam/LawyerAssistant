using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Queries;

public class GetComplexByIdQuery : IRequest<SysResult<GetComplexDTO>>
{
    [Required(ErrorMessage =ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
}
