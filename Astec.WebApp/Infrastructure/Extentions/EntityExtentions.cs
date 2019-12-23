using Astec.Model.Models;
using Astec.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Astec.WebApp.Infrastructure.Extentions
{
    public static class EntityExtentions
    {
        public static void UpdateApartment(this Apartment apartment, ApartmentViewModel apartmentViewModel)
        {
            apartment.ApartmentID = apartmentViewModel.ApartmentID;
            apartment.ApartmentName = apartmentViewModel.ApartmentName;
            apartment.Description = apartmentViewModel.Description;
            apartment.Location= apartmentViewModel.Location;
            apartment.CreatedDate = apartmentViewModel.CreatedDate;
            apartment.CreatedBy = apartmentViewModel.CreatedBy;
            apartment.UpdatedDate = apartmentViewModel.UpdatedDate;
            apartment.UpdatedBy = apartmentViewModel.UpdatedBy;
            apartment.MetaKeyword = apartmentViewModel.MetaKeyword;
            apartment.MetaDescription = apartmentViewModel.MetaDescription;
            apartment.Status = apartmentViewModel.Status;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }

        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {

            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDate = appUserViewModel.BirthDate;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }

        public static void UpdateModule(this ApplicationModule appModule, ApplicationModuleViewModel appModuleViewModel, string action = "add")
        {

            appModule.Id = appModuleViewModel.Id;
            appModule.Name = appModuleViewModel.Name;
            appModule.Url = appModuleViewModel.Url;
            appModule.ParentId = appModuleViewModel.ParentId;
        }
    }
}