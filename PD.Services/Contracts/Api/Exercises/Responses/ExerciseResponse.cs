using PD.Services.Contracts.Api.ExercisesDetails.Response;
using PD.Services.Contracts.Api.ExercisesEquipments.Responses;
using PowerlifterDiary.Models;

namespace PD.Services.Contracts.Api.Exercises.Responses
{
    public class ExerciseResponse
    {
        public ExerciseResponse(Exercise exercise)
        {
            Id = exercise.Id;
            Name = exercise.Name;
            Description = exercise.Description;
            if(exercise.ExerciseEquipment!=null)
            {
                ExerciseEquipment = new ExerciseEquipmentResponse(exercise.ExerciseEquipment);
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ExerciseEquipmentResponse ExerciseEquipment { get; set; }
    }
}
