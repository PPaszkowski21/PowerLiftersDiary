using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Users.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
    }
}
