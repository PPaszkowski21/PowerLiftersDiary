﻿using PD.Services;
using PD.Services.Contracts.Api.Diaries.Requests;
using PD.Services.Interfaces;
using PD.Services.Services;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PD.Api.Controllers
{
    [RoutePrefix("diary")]
    public class DiaryController : BaseApiController
    {
        private readonly ICrudService<IDiary> _diaryService;

        public DiaryController()
        {
            this._diaryService = new DiaryService();
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(AddDiaryRequest diary)
        {
            if (diary == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _diaryService.Add(diary);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
        //[HttpGet]
        //[Route("get")]
        //public Diary GetDiary(int id)
        //{
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        Diary diary = db.Diaries.Where(x => x.Id == id).Include(x => x.User).FirstOrDefault();
        //        return diary;
        //    }
        //}
        [HttpGet]
        [Route("get")]
        public IHttpActionResult ReadById(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _diaryService.ReadById(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
        [HttpGet]
        [Route("read")]
        public IHttpActionResult Read()
        {
            var result = _diaryService.Read();
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _diaryService.Delete(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(UpdateDiaryRequest diary)
        {
            if (diary.Id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _diaryService.Update(diary);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
    }
}