using BOB.Power.Models;
using BOB.Power.Services;
using Microsoft.AspNetCore.Mvc;

namespace BOB.Power.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerController : ControllerBase
{
    private readonly IPowerService _powerService;
    private readonly ILogger<PowerController> _logger;

    public PowerController(IPowerService powerService,ILogger<PowerController> logger)
    {
        _powerService = powerService;
        _logger = logger;
    }

    [HttpGet(Name = "GetSpotPriceNow")]
    public IntervalPricing Get([FromQuery] Decimal poNumber)
    {
               return _powerService.GetSpotPriceNow(poNumber);
    }
}