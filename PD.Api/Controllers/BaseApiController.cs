using PD.Api.Attributes;
using PD.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PD.Api.Controllers
{
    [AuthFilter]
    [CustomHeaderFilter]
    public class BaseApiController : ApiController
    {
        public HttpResponseMessage CreateCustomResponseMessage(ServiceResponse result)
        {
            if (result.StatusCode != HttpStatusCode.Created)
            {
                return Request.CreateResponse(result.StatusCode);
            }
            var response = Request.CreateResponse(result.StatusCode);
            var json = new JavaScriptSerializer().Serialize(result.ResponseMessage);
            response.Content = new StringContent(json);
            return response;
        }

        public HttpResponseMessage CreateCustomResponseMessage<T>(ServiceResponse<T> result)
        {
            var response = Request.CreateResponse(result.StatusCode);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var json = new JavaScriptSerializer().Serialize(result.Content);
                response.Content = new StringContent(json);
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(result.ResponseMessage);
                response.Content = new StringContent(json);
            }

            return response;
        }

        public HttpResponseMessage CreateCustomResponseMessage(HttpStatusCode statusCode)
        {
            return Request.CreateResponse(statusCode);
        }
    }
}