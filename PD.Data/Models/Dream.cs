using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class Dream
    {
        [ForeignKey("Day")]
        public int Id { get; set; }
        public float Length { get; set; }
        public string Quality { get; set; }
        public virtual Day Day { get; set; }
    }
}