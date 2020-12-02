using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.UserDetails.Requests
{
    public class AddUserDetailsRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public int Age { get; set; }
        public float BMR { get; set; }
        public float BMI { get; set; }
    }
}
