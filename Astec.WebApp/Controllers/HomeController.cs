using Astec.Service;
using Astec.WebApp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Astec.WebApp.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiControllerBase
    {
        IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, TEDU Member. ";
        }
    }
}