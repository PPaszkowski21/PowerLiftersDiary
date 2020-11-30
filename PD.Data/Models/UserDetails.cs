using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class UserDetails
    {
        [ForeignKey("User")]
        public virtual int Id { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
        public float BMR { get; set; }
        public float BMI { get; set; }

        public virtual User User { get; set; }
    }
}