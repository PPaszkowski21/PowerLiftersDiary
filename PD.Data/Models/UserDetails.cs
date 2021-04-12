using PD.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual Avatar Avatar { get; set; }
        public virtual User User { get; set; }
    }
}