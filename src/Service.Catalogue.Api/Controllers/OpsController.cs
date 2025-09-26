using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Catalogue.Api.Data;

[ApiController]
[Route("api/ops")]
public class OpsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public OpsController(AppDbContext dbContext) => _dbContext = dbContext;

    [HttpGet("health")]
    public IActionResult Health() => Ok(new { status = "ok" });

    [HttpGet("ready")]
    public IActionResult Ready() => Ok(new { status = "ready" });

    [HttpGet("metrics")]
    public IActionResult Metrics() => Ok(new { servicesCount = _dbContext.Services.Count() });
}
