using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerlifterDiary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public virtual UserDetails UserDetails { get; set; }
        public ICollection<Diary> Diaries { get; set; }
    }
}