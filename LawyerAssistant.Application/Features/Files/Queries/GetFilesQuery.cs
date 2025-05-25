using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Files.Queries;

public class GetFilesQuery : PagingRequest, IRequest<SysResult<PagingResponse<FilesListDto>>>
{
    public string Title { get; set; }
}
