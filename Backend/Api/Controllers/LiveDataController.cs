using Api.SignalRConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class LiveDataController : ControllerBase
    {
        private readonly IHubContext<DataReadingHub> _dataReadingHub;
        private readonly IHubContext<WarningHub> _warningHub;

        public LiveDataController(IHubContext<DataReadingHub> dataReadingHub, IHubContext<WarningHub> warningHub)
        {
            _dataReadingHub = dataReadingHub;
            _warningHub = warningHub;
        }

        [HttpPost("livePatientMeasure/send")]
        public async Task<IActionResult> PostSensorData([FromBody] LiveDataReading liveDataReading)
        {

            await _dataReadingHub.Clients.All.SendAsync("ReceivePatientReadingData", liveDataReading);

            return Ok();
        }

        [HttpPost("liveWarning/send")]
        public async Task<IActionResult> PostSensorData([FromBody] LiveWarning liveWarning)
        {

            await _warningHub.Clients.All.SendAsync("ReceiveWarningData", liveWarning);

            return Ok();
        }
    }
}
