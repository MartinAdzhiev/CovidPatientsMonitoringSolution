using Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/dataReading")]
    [ApiController]
    public class DataReadingController : ControllerBase
    {
        private readonly IDataReadingService _dataReadingService;

        public DataReadingController(IDataReadingService dataReadingService)
        {
            _dataReadingService = dataReadingService;
        }


        [HttpGet]
        [Route("filterDataReadingsAfterDate")]
        public async Task<IActionResult> GetMeasuresAfterInterval([FromQuery] int patientMeasureId, DateTime dateTime)
        {
            if (patientMeasureId < 0)
            {
                return BadRequest("Invalid patientMeasure id.");
            }

            var dataReadings = await _dataReadingService.GetDataReadingsAfterDate(patientMeasureId, dateTime);

            if (dataReadings == null || dataReadings.Count == 0)
            {
                return NotFound("No data found for the given patient and date interval.");
            }

            return Ok(dataReadings);
        }

        [HttpGet]
        [Route("filterMeasuresWithinRange")]
        public async Task<IActionResult> GetMeasuresWithinInterval([FromQuery] int patientMeasureId, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            if (patientMeasureId < 0 || dateTimeFrom >= dateTimeTo)
            {
                return BadRequest("Invalid patientMeasure id or date range.");
            }

            var dataReadings = await _dataReadingService.GetDataReadingsWithinInterval(patientMeasureId, dateTimeFrom, dateTimeTo);

            if (dataReadings == null || dataReadings.Count == 0)
            {
                return NotFound("No data found for the given patient and date range.");
            }

            return Ok(dataReadings);
        }

        [HttpGet]
        [Route("lastTen")]
        public async Task<IActionResult> LastTenReadings([FromQuery] int patientMeasureId)
        {
            if (patientMeasureId < 0)
            {
                return BadRequest("Invalid patientMeasure id or date range.");
            }

            var dataReadings = await _dataReadingService.LastTenReadingsForPatientMeasure(patientMeasureId);

            return Ok(dataReadings);
        }

        [HttpGet]
        [Route("latest")]
        public async Task<IActionResult> Latest()
        {

            var dataReadings = await _dataReadingService.LastReadingForAllPatientMeasures();

            return Ok(dataReadings);
        }
    }
}
