using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.UserDetails.Requests
{
    public class UpdateUserDetailsRequest
    {
        [Required]
        public int UserId { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
    }
}
