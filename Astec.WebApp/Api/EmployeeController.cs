using Astec.Common;
using Astec.Data.Infastructure;
using Astec.Model.Models;
using Astec.Service;
using Astec.WebApp.Infrastructure.Core;
using Astec.WebApp.Infrastructure.Extentions;
using Astec.WebApp.Models;
using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Astec.WebApp.Api
{
    [RoutePrefix("api/employee")]
    [Authorize(Roles ="admin")]
    public class EmployeeController : ApiControllerBase
    {
        private readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));
        IEmployeeService _employeeService;
        public EmployeeController(IErrorService errorService, IEmployeeService employeeService) : base(errorService)
        {
            _employeeService = employeeService;
        }

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage Get(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _employeeService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<EmployeeViewModel> modelVm = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(model);

                PaginationSet<EmployeeViewModel> pagedSet = new PaginationSet<EmployeeViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };
                
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [Route("getlistpaging")]
        [Authorize(Roles = "View")]
        [HttpGet]

        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                var identity = (ClaimsIdentity)User.Identity;
                HttpResponseMessage response = null;
                int totalRow = 0;
                try
                {
                    var model = _employeeService.GetAll(page, pageSize, out totalRow, filter);
                    IEnumerable<EmployeeViewModel> modelVm = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(model);

                    PaginationSet<EmployeeViewModel> pagedSet = new PaginationSet<EmployeeViewModel>()
                    {
                        Page = page,
                        TotalCount = totalRow,
                        TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                        Items = modelVm
                    };
                    response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                    log.Info("UserName: " + identity.Name+"," +" Data: "+ response);
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message);
                }              
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
                var model = _employeeService.GetById(id);
                var responseData = Mapper.Map<Employee, EmployeeViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = "Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, EmployeeViewModel employeeViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                var identity = (ClaimsIdentity)User.Identity;
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    log.Error("UserName: " + identity.Name + "," + " Data: " + response);
                }
                else
                {
                    Employee newEmployee = new Employee();
                    newEmployee.UpdateEmployee(employeeViewModel);
                    newEmployee.CreatedDate = DateTime.Now;
                    newEmployee.CreatedBy = identity.Name;
                    _employeeService.Add(newEmployee);
                    _employeeService.SaveChanges();
                    var responseData = Mapper.Map<Employee, EmployeeViewModel>(newEmployee);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    log.Info("UserName: " + identity.Name + "," + " Data: " + response);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        [Authorize(Roles = "Edit")]
        public HttpResponseMessage Update(HttpRequestMessage request, EmployeeViewModel employeeViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                var identity = (ClaimsIdentity)User.Identity;
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    log.Error("UserName: " + identity.Name + "," + " Data: " + response);
                }
                else
                {
                    var db = _employeeService.GetById(employeeViewModel.Id);

                    db.UpdateEmployee(employeeViewModel);
                    db.UpdatedDate = DateTime.Now;
                    db.UpdatedBy = identity.Name;

                    _employeeService.Update(db);
                    _employeeService.SaveChanges();

                    var responseData = Mapper.Map<Employee, EmployeeViewModel>(db);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    log.Info("UserName: " + identity.Name + "," + " Data: " + response);
                }
               
                return response;
            });
        }

        [Route("delete")]
        [HttpPost]
        [Authorize(Roles = "Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var identity = (ClaimsIdentity)User.Identity;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    log.Error("UserName: " + identity.Name + "," + " Data: " + response);
                }
                else
                {
                    var oldEmployee = _employeeService.Delete(id);
                    _employeeService.SaveChanges();

                    var responseData = Mapper.Map<Employee, EmployeeViewModel>(oldEmployee);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    log.Info("UserName: " + identity.Name + "," + " Data: " + response);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request, string filter = null)
        {
            string fileName = string.Concat("Employee_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _employeeService.GetListEmloyee(filter).ToList();
                await ReportHelper.GenerateXls(data, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("ExportPdf")]
        public async Task<HttpResponseMessage> ExportPdf(HttpRequestMessage request, int id)
        {
            string fileName = string.Concat("Employee" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".pdf");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var template = File.ReadAllText(HttpContext.Current.Server.MapPath("/Assets/admin/templates/employee-detail.html"));
                var replaces = new Dictionary<string, string>();
                var employee = _employeeService.GetById(id);
                var g = "";
                if(employee.Gender)
                    g = "Nam";
                g = "Nữ";

                replaces.Add("{{Name}}", employee.Name);
                replaces.Add("{{DateOfBirth}}", employee.DateOfBirth.ToString("dd-MM-yyyy"));
                replaces.Add("{{Gender}}", g);
                replaces.Add("{{Address}}", employee.Address);
                template = template.Parse(replaces);
                await ReportHelper.GeneratePdf(template, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    } 
}
