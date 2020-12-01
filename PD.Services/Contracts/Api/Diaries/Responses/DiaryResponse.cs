using PD.Services.Contracts.Api.Days.Responses;
using PD.Services.Contracts.Api.Users.Responses;
using PD.Services.Interfaces;
using PD.Services.Services;
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
        public int UserId { get; set; }
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
        public ICollection<DayResponse> Days { get; set; }

        public DiaryResponse(Diary diary, Type type)
        {
            Id = diary.Id;
            if (type == typeof(DiaryResponse))
            {
                UserId = diary.User.Id;
            }
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
            if(type == typeof(DiaryResponse))
            {
                Days = new List<DayResponse>();
                if (diary.Days != null)
                {
                    DayService dayService = new DayService();
                    foreach (var day in diary.Days)
                    {
                        Days.Add(dayService.GetDay(day.Id));
                    }
                }
            }
        }
    }
}
