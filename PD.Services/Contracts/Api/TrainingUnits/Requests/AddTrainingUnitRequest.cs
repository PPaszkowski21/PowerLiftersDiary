using PD.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.TrainingUnits.Requests
{
    public class AddTrainingUnitRequest : ITrainingUnit
    {
        [Required]
        public int DayId { get; set; }
    }
}
