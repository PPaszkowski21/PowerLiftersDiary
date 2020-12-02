using System;
using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Days.Requests
{
    public class UpdateDayRequest
    {
        [Required]
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
