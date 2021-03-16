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
        public string DateRange { get; set; }
        public List<ExerciseStatus> WeekSummary { get; set; }
        public WeekSummaryResponse(string dateRange, List<ExerciseStatus> weekSummary)
        {
            DateRange = dateRange;
            WeekSummary = weekSummary;
        }
    }
}
