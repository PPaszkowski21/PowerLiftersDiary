using PD.Services.Contracts.Api.Diaries.Responses;
using PD.Services.Contracts.Api.Dreams.Responses;
using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Days.Responses
{
    public class DayResponse : IDay
    {
        public int Id { get; set; }
        public DiaryResponse Diary { get; set; }
        public DateTime Date { get; set; }
        public virtual DreamResponse Dream { get; set; }
        public ICollection<TrainingUnit> TrainingUnits { get; set; }

        public DayResponse(Day day, Type type)
        {
            this.Id = day.Id;
            if (type == typeof(DayResponse))
            {
                this.Diary = new DiaryResponse(day.Diary, typeof(DayResponse));
                this.Dream = new DreamResponse(day, typeof(DayResponse));
            }
            this.Date = day.Date;
        }
    }
}
