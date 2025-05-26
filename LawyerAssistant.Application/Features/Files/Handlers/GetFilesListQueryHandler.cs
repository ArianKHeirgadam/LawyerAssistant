using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.Files.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.Files.Handlers;

public class GetFilesListQueryHandler : IRequestHandler<GetFilesListQuery, SysResult<List<GenericDTO>>>
{
    private readonly IRepository<FilesModel> _repository;
    public GetFilesListQueryHandler(IRepository<FilesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<List<GenericDTO>>> Handle(GetFilesListQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(c => !string.IsNullOrWhiteSpace(request.Title) ? c.Title.ToLower().Contains(request.Title.ToLower()) : true)
             .Select(c => new GenericDTO
             {
                 Title = c.Title,
                 Id = c.Id,
             }).ToListAsync();


        return new SysResult<List<GenericDTO>>() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully, Value = result };
    }
}
