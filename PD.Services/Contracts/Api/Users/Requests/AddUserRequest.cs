using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Users.Requests
{
    public class AddUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string City { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
