using PD.Services;
using PD.Services.Contracts.Api.TrainingUnits.Requests;
using PD.Services.Interfaces;
using PD.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace PD.Api.Controllers
{
    [RoutePrefix("training")]
    public class TrainingController : BaseApiController
    {
        private readonly ICrudService<ITrainingUnit> _crudService;

        public TrainingController()
        {
            this._crudService = new TrainingService();
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(AddTrainingUnitRequest trainingUnitRequest)
        {
            if (trainingUnitRequest == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _crudService.Add(trainingUnitRequest);
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