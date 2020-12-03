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
        public DreamResponse(Dream dream)
        {
            if(dream != null)
            {
                Id = dream.Id;
                Length = dream.Length;
                Quality = dream.Quality;
            }
        }
        public DreamResponse(Day day)
        {
            if (day.Dream != null)
            {
                Id = day.Dream.Id;
                Length = day.Dream.Length;
                Quality = day.Dream.Quality;
            }
        }
    }
}
