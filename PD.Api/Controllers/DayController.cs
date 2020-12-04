using PD.Services.Contracts.Api.Days.Requests;
using PD.Services.Contracts.Api.Dreams.Requests;
using PD.Services.Services;
using System.Net;
using System.Web.Http;

namespace PD.Api.Controllers
{
    [RoutePrefix("day")]
    public class DayController : BaseApiController
    {
        private readonly DayService _dayService;

        public DayController()
        {
            this._dayService = new DayService();
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(AddDayRequest day)
        {
            if (day == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _dayService.Add(day);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        //[HttpPut]
        //[Route("update")]
        //public IHttpActionResult Update(UpdateDayRequest day)
        //{
        //    if (day == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
        //    var result = _dayService.Update(day);
        //    return ResponseMessage(CreateCustomResponseMessage(result));
        //}

        //[HttpGet]
        //[Route("read")]
        //public IHttpActionResult Read()
        //{
        //    var result = _dayService.Read();
        //    return ResponseMessage(CreateCustomResponseMessage(result));
        //}

        //[HttpGet]
        //[Route("get")]
        //public IHttpActionResult ReadById(int id)
        //{
        //    if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
        //    var result = _dayService.ReadById(id);
        //    return ResponseMessage(CreateCustomResponseMessage(result));
        //}

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result =_dayService.Delete(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpPost]
        [Route("adddream")]
        public IHttpActionResult AddDream(AddDreamRequest day)
        {
            if (day == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _dayService.AddDream(day);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        //[HttpPut]
        //[Route("updatedream")]
        //public IHttpActionResult UpdateDream(UpdateDreamRequest day)
        //{
        //    if (day == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
        //    var result = _dayService.UpdateDream(day);
        //    return ResponseMessage(CreateCustomResponseMessage(result));
        //}
    }
}