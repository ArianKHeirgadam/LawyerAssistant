using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Queries;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using LawyerAssistant.Application.Features.BaseDefinitions.FileTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers.BaseDefinitions;

[Authorize]
[ApiController]
[Route("base/[controller]")]
public class FileTypesController : ControllerBase
{
    private readonly ISender _sender;
    public FileTypesController(ISender sender) => _sender = sender;


    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetFileTypeQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetFileTypeByIdQuery() { Id = id });
        return Ok(result);
    }
    [HttpGet("select-box")]
    public async Task<IActionResult> GetSelectBox([FromQuery] GetFileTypesListQuery query)
    {
        var result = await _sender.Send(query);
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFileTypeCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFileTypeCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteInputDTO input)
    {
        var result = await _sender.Send(new DeleteFileTypeCommand() { Ids = input.Ids });
        return Ok(result);
    }
    #endregion

}
