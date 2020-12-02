using PD.Services.Contracts.Api.Days.Responses;
using PowerlifterDiary.Models;
using System;

namespace PD.Services.Contracts.Api.Dreams.Responses
{
    public class DreamResponse
    {
        public int Id { get; set; }
        public float Length { get; set; }
        public string Quality { get; set; }
        public virtual DayResponse Day { get; set; }
        public DreamResponse(Dream dream, Type type)
        {
            if(dream != null)
            {
                Id = dream.Id;
            }
            Length = dream.Length;
            Quality = dream.Quality;
            if(type == typeof(DreamResponse))
            {
                this.Day = new DayResponse(dream.Day, typeof(DreamResponse));
            }
        }
        public DreamResponse()
        {
                
        }

        public DreamResponse(Day day, Type type)
        {
            if (day.Dream != null)
            {
                if (day.Dream != null)
                {
                    Id = day.Dream.Id;
                }
                Length = day.Dream.Length;
                Quality = day.Dream.Quality;
                if (type == typeof(DreamResponse))
                {
                    this.Day = new DayResponse(day.Dream.Day, typeof(DreamResponse));
                }
            }
        }
    }
}
