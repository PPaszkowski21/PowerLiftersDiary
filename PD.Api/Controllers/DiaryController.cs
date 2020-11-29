using PD.Services;
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
        private readonly ICrudService<IDiary> _crudService;

        public DiaryController()
        {
            this._crudService = new DiaryService();
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(AddDiaryRequest diary)
        {
            if (diary == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _crudService.Add(diary);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
        [HttpGet]
        [Route("get")]
        public Diary GetDiary(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                Diary diary = db.Diaries.Where(x => x.Id == id).Include(x => x.User).FirstOrDefault();
                return diary;
            }
        }

    }
}