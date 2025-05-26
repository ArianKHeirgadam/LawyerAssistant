using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Features.ReActions.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ReactionController : ControllerBase
{
    private readonly ISender _sender;
    public ReactionController(ISender sender) => _sender = sender;

    #region Commands
    /// <summary>
    /// create the reaction 
    /// </summary>
    /// <param name="command">create the reaction </param>
    /// <returns> System result </returns>
    /// <remarks>
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> CreateReaction([FromBody] CreateReActionDto command)
    {
        var response = await _sender.Send(new CreateReActionCommand() { Dto = command });
        return Ok(response);
    }

    /// <summary>
    /// change the reaction status 
    /// </summary>
    /// <param name="command">change the reaction status </param>
    /// <returns> System result </returns>
    /// <remarks>
    /// </remarks>

    [HttpPost("status")]
    public async Task<IActionResult> ChangeReaction([FromBody] ChangeReactionStatusCommand command)
    {
        var response = await _sender.Send(command);
        return Ok(response);
    }
    #endregion
}
