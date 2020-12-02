using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Dreams.Requests
{
    public class UpdateDreamRequest
    {
        [Required]
        public int Id { get; set; }
        public float Length { get; set; }
        public string Quality { get; set; }
    }
}
