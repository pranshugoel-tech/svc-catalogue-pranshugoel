using Microsoft.AspNetCore.Mvc;
using Service.Catalogue.Api.Models;
using Service.Catalogue.Api.Services;

namespace Service.Catalogue.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceCatalogueService _service;
        private readonly ICsvImportService _csvService;
        public ServiceController(IServiceCatalogueService service, ICsvImportService csvService)
        {
            _csvService = csvService;
            _service = service;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Get([FromQuery] string? ownerTeam, string? tier, string? lifecycle, string? search)
        {
            var services = await _service.SearchAsync(ownerTeam, tier, lifecycle, search);
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            {
                var svc = await _service.GetByIdAsync(id);
                return svc == null ? NotFound() : Ok(svc);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            {
                var svc = await _service.GetByNameAsync(name);
                return svc == null ? NotFound() : Ok(svc);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ServiceCatalogueArgument serviceCatalogueArg)
        {
            try
            {
                var result = await _service.CreateAsync(serviceCatalogueArg);

                return CreatedAtAction(nameof(Get), result);               
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceCatalogueArgument serviceCatalogueArg)
        {
            try
            {
                var result = await _service.UpdateAsync(id, serviceCatalogueArg);
                return CreatedAtAction(nameof(Get), result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok("Record Deleted");
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0) return BadRequest("CSV file required");
                using var stream = file.OpenReadStream();
                var count = await _csvService.ImportCsvAsync(stream);
                return Ok(new { imported = count });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}