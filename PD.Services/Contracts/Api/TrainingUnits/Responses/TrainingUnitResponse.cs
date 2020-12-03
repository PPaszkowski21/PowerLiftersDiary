using PD.Services.Contracts.Api.ExerciseTrainings.Responses;
using PD.Services.Services;
using PowerlifterDiary.Models;
using System.Collections.Generic;

namespace PD.Services.Contracts.Api.TrainingUnits.Responses
{
    public class TrainingUnitResponse
    {
        public int Id { get; set; }
        public ICollection<ExerciseTrainingResponse> ExerciseTrainings { get; set; }
        public TrainingUnitResponse(TrainingUnit trainingUnit)
        {
            Id = trainingUnit.Id;
            if (trainingUnit != null)
            {
                ExerciseTrainings = new List<ExerciseTrainingResponse>();
                TrainingService trainingService = new TrainingService();
                foreach (var exerciseTraining in trainingUnit.ExerciseTrainings)
                {
                    ExerciseTrainings.Add(trainingService.GetExerciseTraining(exerciseTraining.Id));
                }
            }
        }

    }
}
