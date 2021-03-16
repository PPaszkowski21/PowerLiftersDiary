using PD.Services.Contracts.Api.Diaries.Requests;
using PD.Services.Services;
using System.Net;
using System.Web.Http;

namespace PD.Api.Controllers
{
    [RoutePrefix("diary")]
    public class DiaryController : BaseApiController
    {
        private readonly DiaryService _diaryService;

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
        //public IHttpActionResult ReadById(int id)
        //{
        //    if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
        //    var result = _diaryService.ReadById(id);
        //    return ResponseMessage(CreateCustomResponseMessage(result));
        //}
        //[HttpGet]
        //[Route("read")]
        //public IHttpActionResult Read()
        //{
        //    var result = _diaryService.Read();
        //    return ResponseMessage(CreateCustomResponseMessage(result));
        //}
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
        [HttpPost]
        [Route("weeksummary")]
        public IHttpActionResult WeekSummary(int diaryId, int dayId)
        {
            var result = _diaryService.WeekSummary(diaryId, dayId);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
    }
}