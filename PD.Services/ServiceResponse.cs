using System.Net;

namespace PD.Services
{
    public class ServiceResponse
    {
        public ServiceResponse(HttpStatusCode statusCode, string responseMessage = "")
        {
            ResponseMessage = responseMessage;
            StatusCode = statusCode;
        }

        public ServiceResponse()
        {
        }
        public string ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
    public class ServiceResponse<T>
    {
        public ServiceResponse(T content, HttpStatusCode statusCode, string responseMessage = "")
        {
            Content = content;
            ResponseMessage = responseMessage;
            StatusCode = statusCode;
        }

        public ServiceResponse()
        {
        }

        public T Content { get; set; }
        public string ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
