using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Days.Requests
{
    public class AddDayRequest : IDay
    {
        [Required]
        public int DiaryId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        //public AddDayRequest(int diaryId, DateTime dateTime)
        //{
        //    this.DiaryId = DiaryId;
        //    this.Date = Date;
        //}
    }
}
