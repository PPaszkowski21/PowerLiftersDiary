using System;
using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Diaries.Requests
{
    public class UpdateDiaryRequest
    {
        [Required]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Conclusions { get; set; }
        public int BenchPressStart { get; set; }
        public int SquatStart { get; set; }
        public int DeadliftStart { get; set; }
        public int BenchPressEnd { get; set; }
        public int SquatEnd { get; set; }
        public int DeadliftEnd { get; set; }
        public float Progress { get; set; }
    }
}
