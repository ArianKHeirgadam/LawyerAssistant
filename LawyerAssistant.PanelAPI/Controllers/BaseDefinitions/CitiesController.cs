using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers.BaseDefinitions;

[Authorize]
[ApiController]
[Route("base/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ISender _sender;

    public CitiesController(ISender sender) => _sender = sender;
    
    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCitiesQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetCityByIdQuery() { Id = id });
        return Ok(result);
    }
    #endregion

}
