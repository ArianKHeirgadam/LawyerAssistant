using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers.BaseDefinitions;

[Authorize]
[ApiController]
[Route("base/[controller]")]
public class DemandsController : ControllerBase
{
    private readonly ISender _sender;
    public DemandsController(ISender sender) => _sender = sender;


    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetDemandsQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetDemandsByIdQuery() { Id = id });
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDemandsCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDemandsCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteDemandsCommand() { Id = id });
        return Ok(result);
    }
    #endregion
}
