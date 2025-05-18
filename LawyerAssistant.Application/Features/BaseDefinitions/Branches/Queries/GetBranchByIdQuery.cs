using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Queries;

public class GetBranchByIdQuery : IRequest<SysResult<GetBranchDTO>>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
}
