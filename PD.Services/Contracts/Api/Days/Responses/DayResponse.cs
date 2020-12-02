using PD.Services.Contracts.Api.Diaries.Responses;
using PD.Services.Contracts.Api.Dreams.Responses;
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
            if (type == typeof(DayResponse))
            {
                Diary = new DiaryResponse(day.Diary, typeof(DayResponse));
                Dream = new DreamResponse(day, typeof(DayResponse));   
            }
            Date = day.Date;
        }
        public int Id { get; set; }
        public DiaryResponse Diary { get; set; }
        public DateTime Date { get; set; }
        public virtual DreamResponse Dream { get; set; }
        public ICollection<TrainingUnit> TrainingUnits { get; set; }

    }
}
