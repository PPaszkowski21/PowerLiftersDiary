using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerlifterDiary.Models
{
    public class Diary
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

    }
}