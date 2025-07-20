using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.Identities.Customers.Commands;
using LawyerAssistant.Application.Features.Identities.Customers.Queries;
using LawyerAssistant.Application.Objects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.PanelAPI.Controllers.Identities;

[Authorize]
[ApiController]
[Route("Identities/[controller]")]
public class CustomerController  : ControllerBase
{
    private readonly ISender _sender;
    private readonly IOptions<AppConfig> _appConfig;

    public CustomerController(ISender sender, IOptions<AppConfig> appConfig)
    {
        _sender = sender;
        _appConfig = appConfig;
    }

    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCustomersQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _sender.Send(new GetCustomerDetailsQuery() { Id = id });
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteInputDTO input)
    {
        var result = await _sender.Send(new DeleteCustomerCommand() { Ids = input.Ids });
        return Ok(result);
    }
    #endregion
}
