using PD.Services.Contracts.Api.Days.Responses;
using PowerlifterDiary.Models;
using System;

namespace PD.Services.Contracts.Api.TrainingUnits.Responses
{
    public class TrainingUnitResponse
    {
        public int Id { get; set; }
        public DayResponse Day { get; set; }
        //public virtual ICollection<ExerciseTraining> ExerciseTrainings { get; set; }
        public TrainingUnitResponse(TrainingUnit trainingUnit, Type type)
        {
            this.Id = trainingUnit.Id;
            if (type == typeof(TrainingUnitResponse))
            {
                this.Day = new DayResponse(trainingUnit.Day, typeof(TrainingUnitResponse));
            }
        }

    }
}
