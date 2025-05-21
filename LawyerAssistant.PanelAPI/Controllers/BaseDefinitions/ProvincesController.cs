using LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAssistant.PanelAPI.Controllers.BaseDefinitions;

[Authorize]
[ApiController]
[Route("base/[controller]")]
public class ProvincesController : ControllerBase
{
    private readonly ISender _sender;

    public ProvincesController(ISender sender) => _sender = sender;


    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProvincesQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetProvinceByIdQuery() { Id = id });
        return Ok(result);
    }
    #endregion

}
