using Domain.Entities;

namespace Application.Dtos.Responses
{
    public class DeviceResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<PatientMeasureResponse> PatientMeasuresResponses { get; set; }
    }
}
