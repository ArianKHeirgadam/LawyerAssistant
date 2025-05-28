using LawyerAssistant.Application.Contracts.Infrastructure;
using LawyerAssistant.Application.Features.ReActions.Queries;
using LawyerAssistant.Application.Objects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.PanelAPI.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IKavenegarCreditService _kavenegarCreditService;
    private readonly IOptions<AppConfig> _options;
    public DashboardController(ISender sender, IKavenegarCreditService kavenegarCreditService, IOptions<AppConfig> options)
    {
        _sender = sender;
        _kavenegarCreditService = kavenegarCreditService;
        _options = options;
    }


    #region Query
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetReactionQuery Query)
    {
        var result = await _sender.Send(Query);
        return Ok(result);
    }
    [HttpGet("sms-credit")]
    public async Task<IActionResult> GetCredit()
    {
        var value = await _kavenegarCreditService.Inquiry(_options.Value.smsApiKey);
        return Ok(value);
    }
    #endregion
}
