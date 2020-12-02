using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PD.Api.Attributes
{
    public class CustomHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response?.Content == null)
            {
                return;
            }
            actionExecutedContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
        }
    }
}