using Astec.Service;
using Astec.WebApp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Astec.WebApp.Api
{
    [RoutePrefix("api/statistic")]
    
    public class StatisticController : ApiControllerBase
    {
        private IStatisticService _statisticService;
        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }

        /// <summary>
        /// Api thống kê nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate">Từ ngày</param>
        /// <param name="toDate">Đến ngày</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getemployee")]
        public HttpResponseMessage GetEmployeeStatistic(HttpRequestMessage request, string fromDate, string toDate)
        {
            return CreateHttpResponse(request,()=>
            {
                var model = _statisticService.GetEmployeeStatistic(fromDate, toDate);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}
