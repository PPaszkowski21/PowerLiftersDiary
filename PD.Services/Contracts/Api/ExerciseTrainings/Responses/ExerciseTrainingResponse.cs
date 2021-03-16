using PD.Services.Contracts.Api.Exercises.Responses;
using PD.Services.Contracts.Api.ExercisesDetails.Response;
using PD.Services.Services;
using PowerlifterDiary.Models;

namespace PD.Services.Contracts.Api.ExerciseTrainings.Responses
{
    public class ExerciseTrainingResponse
    {
        public int Id { get; set; }
        public ExerciseResponse Exercise { get; set; }
        public ExerciseDetailsResponse ExerciseDetails { get; set; }
        public ExerciseTrainingResponse(ExerciseTraining exerciseTraining)
        {
            Id = exerciseTraining.Id;
            if(exerciseTraining.Exercise != null)
            {
                ExerciseService exerciseService = new ExerciseService();

                Exercise = exerciseService.GetExercise(exerciseTraining.Exercise.Id);
            }
            if(exerciseTraining.ExerciseDetails != null)
            {
                ExerciseDetails = new ExerciseDetailsResponse(exerciseTraining.ExerciseDetails);
            }
        }

        public ExerciseTrainingResponse()
        {
        }
    }
}
