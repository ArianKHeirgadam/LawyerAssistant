using LawyerAssistant.Application.Features.ReActions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ISender _sender;
    public DashboardController(ISender sender) => _sender = sender;


    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetReactionQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }
    #endregion
}
