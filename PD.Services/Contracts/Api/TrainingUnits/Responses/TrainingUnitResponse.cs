using PD.Services.Contracts.Api.Days.Responses;
using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.TrainingUnits.Responses
{
    public class TrainingUnitResponse : ITrainingUnit
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
