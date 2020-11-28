using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Interfaces
{
    public interface IDiary
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Conclusions { get; set; }
        int BenchPressStart { get; set; }
        int SquatStart { get; set; }
        int DeadliftStart { get; set; }
        int BenchPressEnd { get; set; }
        int SquatEnd { get; set; }
        int DeadliftEnd { get; set; }
        float Progress { get; set; }
    }
}
