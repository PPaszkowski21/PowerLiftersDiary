using Microsoft.AspNet.Identity.EntityFramework;
using PD.Data.Models;
using System.Collections.Generic;

namespace PowerlifterDiary.Models
{
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public new int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public virtual UserDetails UserDetails { get; set; }
        public virtual ICollection<Diary> Diaries { get; set; }
    }
}