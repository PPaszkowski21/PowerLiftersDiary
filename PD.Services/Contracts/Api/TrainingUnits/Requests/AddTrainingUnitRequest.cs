using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.TrainingUnits.Requests
{
    public class AddTrainingUnitRequest
    {
        [Required]
        public int DayId { get; set; }
    }
}
