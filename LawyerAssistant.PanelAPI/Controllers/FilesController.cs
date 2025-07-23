using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.Files.Commands;
using LawyerAssistant.Application.Features.Files.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly ISender _sender;
    public FilesController(ISender sender) => _sender = sender;

    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetFilesQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetFilesByIdQuery() { Id = id });
        return Ok(result);
    }
    [HttpGet("select-box")]
    public async Task<IActionResult> GetSelectBox([FromQuery] GetFilesListQuery query)
    {
        var result = await _sender.Send(query);
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFileDto command)
    {
        var result = await _sender.Send(new CreateFilesCommand() { Dto = });
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] FilesUpdateDto command)
    {
        var result = await _sender.Send(new UpdateFilesCommand() { Dto = command });
        return Ok(result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteInputDTO input)
    {
        var result = await _sender.Send(new DeleteFilesCommand() { Ids = input.Ids });
        return Ok(result);
    }
    #endregion
}
