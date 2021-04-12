using PD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Diaries.Responses
{
    public class WeekSummaryResponse
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ExerciseStatus> WeekSummary { get; set; }
        public WeekSummaryResponse(string startDate, string endDate, List<ExerciseStatus> weekSummary)
        {
            StartDate = startDate;
            EndDate = endDate;
            WeekSummary = weekSummary;
        }
    }
}
