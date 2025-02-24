﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PowerlifterDiary.Models
{
    public class Day
    {
        public int Id { get; set; }
        public virtual Diary Diary { get; set; }
        public DateTime Date { get; set; }
        public virtual Dream Dream { get; set; }
        public virtual ICollection<TrainingUnit> TrainingUnits { get; set; }
    }
}