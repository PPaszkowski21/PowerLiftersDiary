using PowerlifterDiary.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PD.Data.Models
{
    public class Avatar
    {
        [ForeignKey("UserDetails")]
        public virtual int Id { get; set; }
        public byte[] Image { get; set; }
        public virtual UserDetails UserDetails { get; set; }
    }
}
