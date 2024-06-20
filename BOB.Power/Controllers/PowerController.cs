using BOB.Power.Models;
using BOB.Power.Services;
using Microsoft.AspNetCore.Mvc;

namespace BOB.Power.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerController(IPowerService powerService, ILogger<PowerController> logger)
    : ControllerBase
{
    private readonly ILogger<PowerController> _logger = logger;

    [HttpGet("spot-price-now")]
    public IntervalPricing Get([FromQuery] Decimal poNumber)
    {
               return powerService.GetSpotPriceNow(poNumber);
    }
    [HttpGet("average-spot-price-today")]
    public IntervalPricing GetAvg([FromQuery] Decimal poNumber)
    {
        return powerService.GetAvgSpotPriceToday(poNumber);
    }
    
}