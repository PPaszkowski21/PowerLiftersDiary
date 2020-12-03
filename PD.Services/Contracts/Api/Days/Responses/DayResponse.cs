using PD.Services.Contracts.Api.Dreams.Responses;
using PD.Services.Contracts.Api.TrainingUnits.Responses;
using PD.Services.Services;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;

namespace PD.Services.Contracts.Api.Days.Responses
{
    public class DayResponse
    {
        public DayResponse(Day day, Type type)
        {
            Id = day.Id;
            if(day.Dream != null)
            {
                Dream = new DreamResponse(day);
            }
            if (day.TrainingUnits != null)
            {
                TrainingUnits = new List<TrainingUnitResponse>();
                TrainingService trainingService = new TrainingService();
                foreach (var trainingUnit in day.TrainingUnits)
                {
                    TrainingUnits.Add(trainingService.GetTrainingUnit(trainingUnit.Id));
                }
            }
            Date = day.Date;
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DreamResponse Dream { get; set; }
        public ICollection<TrainingUnitResponse> TrainingUnits { get; set; }

    }
}
