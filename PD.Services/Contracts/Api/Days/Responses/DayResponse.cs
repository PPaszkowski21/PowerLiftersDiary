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
        public Diary Diary { get; set; }
        public DateTime Date { get; set; }
        public virtual Dream Dream { get; set; }
        public ICollection<TrainingUnit> TrainingUnits { get; set; }

        public DayResponse(Day day)
        {
            this.Id = day.Id;
            this.Diary = day.Diary;
            this.Date = day.Date;
            this.Dream = day.Dream;
            this.TrainingUnits = day.TrainingUnits;
        }
    }
}
