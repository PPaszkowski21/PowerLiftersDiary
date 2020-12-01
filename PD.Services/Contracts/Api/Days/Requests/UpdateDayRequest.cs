using PD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Days.Requests
{
    public class UpdateDayRequest : IDay
    {
        [Required]
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
