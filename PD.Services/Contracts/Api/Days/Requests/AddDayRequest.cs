using System;
using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.Days.Requests
{
    public class AddDayRequest
    {
        [Required]
        public int DiaryId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
