using home_control_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace home_control_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TotalsController : ControllerBase
{
    private readonly ITotalsService _totalsService;

    public TotalsController(ITotalsService totalsService)
    {
        _totalsService = totalsService;
    }

    [HttpGet("persons")]
    public async Task<IActionResult> GetTotalsPerPerson()
    {
        var report = await _totalsService.GetTotalsPerPersonAsync();
        return Ok(report);
    }
}
