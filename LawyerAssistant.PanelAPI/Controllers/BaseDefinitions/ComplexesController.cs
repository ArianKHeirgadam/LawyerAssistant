using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers.BaseDefinitions;

[Authorize]
[ApiController]
[Route("base/[controller]")]
public class ComplexesController : ControllerBase
{
    private readonly ISender _sender;

    public ComplexesController(ISender sender) => _sender = sender;

    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetComplexesQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetComplexByIdQuery() { Id = id });
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateComplexCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateComplexCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteComplexCommand() { Id = id });
        return Ok(result);
    }
    #endregion
}
