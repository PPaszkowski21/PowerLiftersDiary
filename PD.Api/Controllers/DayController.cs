using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PD.Services;
using PD.Services.Services;
using System.Web.Http;
using System.Net;
using PD.Services.Contracts.Api.Days.Requests;
using PD.Services.Interfaces;

namespace PD.Api.Controllers
{
    [RoutePrefix("day")]
    public class DayController : BaseApiController
    {
        private readonly ICrudService<IDay> _crudService;

        public DayController()
        {
            this._crudService = new DayService();
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(AddDayRequest day)
        {
            if (day == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _crudService.Add(day);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(UpdateDayRequest day)
        {
            if (day == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _crudService.Update(day);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpGet]
        [Route("read")]
        public IHttpActionResult Read()
        {
            var result = _crudService.Read();
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult ReadById(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _crudService.ReadById(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _crudService.Delete(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
    }
}