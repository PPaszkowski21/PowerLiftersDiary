using PD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Diaries.Requests
{
    public class AddDiaryRequest : IDiary
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Conclusions { get; set; }

        [Required]
        public int BenchPressStart { get; set; }

        [Required]
        public int SquatStart { get; set; }

        [Required]
        public int DeadliftStart { get; set; }

        public int BenchPressEnd { get; set; }

        public int SquatEnd { get; set; }

        public int DeadliftEnd { get; set; }

        public float Progress { get; set; }
    }
}
