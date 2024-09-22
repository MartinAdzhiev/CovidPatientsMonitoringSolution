using Application.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/warning")]
    [ApiController]
    public class WarningController : ControllerBase
    {
        private readonly IWarningService _warningService;
        private readonly IPatientMeasureService _petientMeasureService;

        public WarningController(IWarningService warningService, IPatientMeasureService patientMeasureService)
        {
            _warningService = warningService;
            _petientMeasureService = patientMeasureService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var warnings = await _warningService.GetAllAsync();

            return Ok(warnings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var warning = await _warningService.GetWarningById(id);
            if (warning == null)
            {
                return NotFound();
            }
            return Ok(warning);
        }

        [HttpGet]
        [Route("systemStatus")]
        public async Task<IActionResult> GetSystemStatus()
        {
            var systemStatus = await _warningService.GetSystemStatus();

            return Ok(systemStatus);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var result = await _warningService.DeleteByIdAsync(id);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
