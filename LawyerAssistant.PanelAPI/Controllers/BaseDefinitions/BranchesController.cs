using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Commands;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers.BaseDefinitions;

[Authorize]
[ApiController]
[Route("base/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly ISender _sender;
    public BranchesController(ISender sender) => _sender = sender;


    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetBranchesQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetBranchByIdQuery() { Id = id });
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBranchCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteBranchCommand() { Id = id });
        return Ok(result);
    }
    #endregion
}
