using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Diaries.Responses
{
    public class DiaryResponse : IDiary
    {
        public int Id { get; set; }
        public User User { get; set; }
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
        public ICollection<Day> Days { get; set; }

        public DiaryResponse(Diary diary)
        {
            Id = diary.Id;
            User = diary.User;
            StartDate = diary.StartDate;
            EndDate = diary.EndDate;
            Conclusions = diary.Conclusions;
            BenchPressStart = diary.BenchPressStart;
            SquatStart = diary.SquatStart;
            DeadliftStart = diary.DeadliftStart;
            BenchPressEnd = diary.BenchPressEnd;
            SquatEnd = diary.SquatEnd;
            DeadliftEnd = diary.DeadliftEnd;
            Progress = diary.Progress;
            Days = diary.Days;
        }
    }
}
