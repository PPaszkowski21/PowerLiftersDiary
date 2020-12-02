using PowerlifterDiary.Models;
using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Dreams.Requests
{
    public class AddDreamRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public float Length { get; set; }
        [Required]
        public string Quality { get; set; }
        public virtual Day Day { get; set; }
    }
}
