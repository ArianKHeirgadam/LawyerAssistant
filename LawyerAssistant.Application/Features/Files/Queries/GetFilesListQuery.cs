using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Files.Queries;

public class GetFilesListQuery : IRequest<SysResult<List<GenericDTO>>>
{
    public string? Title { get; set; }
}

