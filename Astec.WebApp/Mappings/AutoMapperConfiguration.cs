using Astec.Model.Models;
using Astec.WebApp.Models;
using AutoMapper;

namespace Astec.WebApp.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Apartment, ApartmentViewModel>();
            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            Mapper.CreateMap<ApplicationModule, ApplicationModuleViewModel>();
            Mapper.CreateMap<Employee, EmployeeViewModel>();
            
        }
    }
}