using Application.Dtos.Requests;
using Application.Service;
using Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientMeasureController : ControllerBase
    {
        private readonly IPatientMeasureService _patientMeasureService;

        public PatientMeasureController(IPatientMeasureService patientMeasureService)
        {
            _patientMeasureService = patientMeasureService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var patientMeasures = await _patientMeasureService.GetAll();

            return Ok(patientMeasures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patientMeasure = await _patientMeasureService.GetById(id);
            if (patientMeasure == null)
            {
                return NotFound();
            }
            return Ok(patientMeasure);
        }

        [HttpPut("{id}/updateThreshold")]
        public async Task<IActionResult> UpdateRoomThreshold([FromRoute] int id, [FromBody] UpdateThresholdRequest updateThresholdRequest)
        {
            bool result = await _patientMeasureService.ChangeThreshold(id, updateThresholdRequest.MinThreshold, updateThresholdRequest.MaxThreshold);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
