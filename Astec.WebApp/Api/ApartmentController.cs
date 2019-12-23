using Astec.Data;
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
    [RoutePrefix("api/apartment")]
    [Authorize]
    public class ApartmentController : ApiControllerBase
    {
        #region Initialize
        private IApartmentService _apartmentService;

        public ApartmentController(IErrorService errorService, IApartmentService apartmentService) : base(errorService)
        {
            this._apartmentService = apartmentService;
        }
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
        [Authorize(Roles = "View")]
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

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles ="View")]
        public HttpResponseMessage Get(HttpRequestMessage request,string keyword, int page, int pageSize=5)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listApartment = _apartmentService.GetAll(keyword);
                totalRow = listApartment.Count();
                var query = listApartment.OrderBy(s => s.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map< IEnumerable < Apartment>, IEnumerable <ApartmentViewModel>>(query);
                var paginationSet = new PaginationSet<ApartmentViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }
        [Route("getbyname/{filter}/{value}")]
        public List<Apartment> GetOrdersByCustName(string filter, string value)
        {
            AstecDbContext ctx = new AstecDbContext();
            List<Apartment> Res = new List<Apartment>();
            switch (filter)
            {
                case "ApartmentName":
                    Res = (from c in ctx.Apartments
                           where c.ApartmentName.StartsWith(value)
                           select c).ToList();
                    break;
                case "Desciption":
                    Res = (from c in ctx.Apartments
                           where c.Description.StartsWith(value)
                           select c).ToList();
                    break;
            }
            return Res;
        }

        // Get theo kiểu không dùng repository
        //[Route("getall1")]
        //[HttpGet]
        //public IHttpActionResult Get1()
        //{
        //    AstecDbContext db = new AstecDbContext();
        //    List<Apartment> apartments = new List<Apartment>();
        //    apartments = db.Apartments.ToList();
        //    var entity = new List<ApartmentViewModel>();
        //    foreach (var temp in apartments)
        //    {
        //        var t = new ApartmentViewModel();
        //        t.ApartmentID = temp.ApartmentID;
        //        t.ApartmentName = temp.ApartmentName;
        //        t.Location = temp.Location;
        //        t.Description = temp.Description;
        //        t.CreatedDate = temp.CreatedDate;
        //        t.CreatedBy = temp.CreatedBy;
        //        t.UpdatedDate = temp.UpdatedDate;
        //        t.UpdatedBy = temp.UpdatedBy;
        //        t.Status = temp.Status;
        //        t.MetaKeyword = temp.MetaKeyword;
        //        t.MetaDescription = temp.MetaDescription;
        //        entity.Add(t);
        //    }
        //    var listPostCategoryVm = Mapper.Map<List<ApartmentViewModel>>(apartments);
        //    return Ok(listPostCategoryVm);

        //}

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = "Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApartmentViewModel apartmentViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                else
                {
                    Apartment newApartment = new Apartment();
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

        [Route("update")]
        [HttpPut]
        [Authorize(Roles = "Edit")]
        public HttpResponseMessage Update(HttpRequestMessage request, ApartmentViewModel productCategoryVm)
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
                    var db = _apartmentService.GetById(productCategoryVm.ApartmentID);

                    db.UpdateApartment(productCategoryVm);
                    db.UpdatedDate = DateTime.Now;
                    db.UpdatedBy = "Administrator";

                    _apartmentService.Update(db);
                    _apartmentService.SaveChanges();

                    var responseData = Mapper.Map<Apartment, ApartmentViewModel>(db);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Authorize(Roles = "Delete")]
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
                    var oldApartment = _apartmentService.Delete(id);
                    _apartmentService.SaveChanges();

                    var responseData = Mapper.Map<Apartment, ApartmentViewModel>(oldApartment);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
