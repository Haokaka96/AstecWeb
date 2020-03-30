using Astec.Model.Models;
using Astec.Service;
using Astec.WebApp.Infrastructure.Core;
using Astec.WebApp.Infrastructure.Extentions;
using Astec.WebApp.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Astec.WebApp.Api
{
    //[RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        #region Initialize
        private IApartmentService _apartmentService;
        private IErrorService _errorService;

        public TestController(IErrorService errorService)
        {
            this._errorService = errorService;
        }
        //public TestController(IErrorService errorService, IApartmentService apartmentService) : base(errorService)
        //{
        //    this._apartmentService = apartmentService;
        //}
        #endregion

        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    Func<HttpResponseMessage> func = () =>
        //     {
        //         var model = _apartmentService.GetAll();

        //         var responseData = Mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentViewModel>>(model);
        //         var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //         return response;
        //     };
        //    return CreateHttpResponse(request, func);
        //}
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _apartmentService.GetById(id);
                var responseData = Mapper.Map<Apartment, ApartmentViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        private HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }

        [Route("api/test/getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _apartmentService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentViewModel>>(query.AsEnumerable());
                var paginationSet = new PaginationSet<ApartmentViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ApartmentViewModel apartmentViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                else
                {
                    var newApartment = new Apartment();
                    newApartment.UpdateApartment(apartmentViewModel);
                    newApartment.CreatedDate = DateTime.Now;
                    newApartment.CreatedBy = "Administrator";
                    _apartmentService.Add(newApartment);
                    _apartmentService.SaveChanges();
                    var responseData = Mapper.Map<Apartment, ApartmentViewModel>(newApartment);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }
    }
}
