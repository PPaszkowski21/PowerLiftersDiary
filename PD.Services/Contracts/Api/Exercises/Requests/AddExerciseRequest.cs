using PowerlifterDiary.Models;
using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Exercises.Requests
{
    public class AddExerciseRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ExerciseEquipmentId { get; set; }
        [Required]
        public string BodyPart { get; set; }
    }
}
