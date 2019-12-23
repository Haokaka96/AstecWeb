using Astec.Model.Models;
using Astec.Service;
using Astec.WebApp.Infrastructure.Core;
using Astec.WebApp.Infrastructure.Extentions;
using Astec.WebApp.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Astec.WebApp.Api
{
    [RoutePrefix("api/module")]
    [Authorize]
    public class ApplicationModuleController : ApiControllerBase
    {
        private IApplicationModuleService _moduleService;

        public ApplicationModuleController(IErrorService errorService, IApplicationModuleService moduleService) : base(errorService)
        {
            this._moduleService = moduleService;
        }

        [Route("getall")]
        [HttpGet]
        //[Authorize(Roles = "View")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listModule = _moduleService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ApplicationModule>, IEnumerable<ApplicationModuleViewModel>>(listModule);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _moduleService.GetById(id);
                var responseData = Mapper.Map<ApplicationModule, ApplicationModuleViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        //[Authorize(Roles = "Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationModuleViewModel moduleViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                else
                {
                    ApplicationModule newModule = new ApplicationModule();
                    newModule.UpdateModule(moduleViewModel);
                    _moduleService.Add(newModule);
                    _moduleService.SaveChanges();
                    var responseData = Mapper.Map<ApplicationModule, ApplicationModuleViewModel>(newModule);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        //[Authorize(Roles = "Edit")]
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationModuleViewModel moduleViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var db = _moduleService.GetById(moduleViewModel.Id);

                    db.UpdateModule(moduleViewModel);
                    _moduleService.Update(db);
                    _moduleService.SaveChanges();

                    var responseData = Mapper.Map<ApplicationModule, ApplicationModuleViewModel>(db);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        //[Authorize(Roles = "Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldModule = _moduleService.Delete(id);
                    _moduleService.SaveChanges();

                    var responseData = Mapper.Map<ApplicationModule, ApplicationModuleViewModel>(oldModule);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
