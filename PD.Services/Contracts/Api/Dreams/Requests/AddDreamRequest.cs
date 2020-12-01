using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Dreams.Requests
{
    public class AddDreamRequest : IDream
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
