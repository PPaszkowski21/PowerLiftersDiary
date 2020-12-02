using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace PD.Api.Attributes
{
    public class AuthFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }

            var token = string.Empty;
            AuthenticationTicket ticket;
            token = actionContext.Request.Headers.Any(x => x.Key == "Authorization")
                ? actionContext.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value
                    .SingleOrDefault().Replace("Bearer ", "")
                : "";

            if (token == string.Empty)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    "Missing 'Authorization' header. Access denied.");
                return;
            }

            ticket = Startup.OAuthBearerOptions.AccessTokenFormat.Unprotect(token);

            if (ticket == null)
            {
                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid token decrypted.");
                return;
            }

            if (!actionContext.Request.Properties.ContainsKey("Ticket"))
            {
                actionContext.Request.Properties.Add(new KeyValuePair<string, object>("Ticket", ticket));
            }
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor
                       .GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}