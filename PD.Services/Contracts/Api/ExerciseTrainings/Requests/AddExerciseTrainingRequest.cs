using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.ExerciseTrainings.Requests
{
    public class AddExerciseTrainingRequest
    {
        [Required]
        public int TrainingUnitId { get; set; }

        [Required]
        public int ExerciseId{ get; set; }
        [Required]
        public int ExerciseDetailsId { get; set; }
    }
}
