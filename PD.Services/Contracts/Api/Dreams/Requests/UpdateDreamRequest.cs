using PD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Dreams.Requests
{
    public class UpdateDreamRequest : IDream
    {
        [Required]
        public int Id { get; set; }
        public float Length { get; set; }
        public string Quality { get; set; }
    }
}
