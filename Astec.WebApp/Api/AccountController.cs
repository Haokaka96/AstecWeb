using Astec.Data;
using Astec.Service;
using Astec.WebApp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Astec.WebApp.Api
{
    [RoutePrefix("api/account")]
    [Authorize(Roles ="admin")]
    public class AccountController : ApiControllerBase
    {
        public AccountController(IErrorService errorService) : base(errorService)
        {
           
        }
        [Route("get")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var identity = (ClaimsIdentity)User.Identity;
                var userName = identity.Name;
                response = request.CreateResponse(HttpStatusCode.OK, userName);

                return response;
            });
        }
    }
}
