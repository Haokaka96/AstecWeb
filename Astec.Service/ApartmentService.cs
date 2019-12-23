using Astec.Data.Infastructure;
using Astec.Data.Repositories;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Service
{
    public interface IApartmentService
    {
        void Add(Apartment apartment);
        void Update(Apartment apartment);
       // void Delete(int id);
        Apartment Delete(int id);
        IEnumerable<Apartment> GetAll();
        IEnumerable<Apartment> GetAll(string keyword);
        IEnumerable<Apartment> GetAllPaging(int page, int pageSize, out int totalRow);
        Apartment GetById(int id);
        void SaveChanges();
    }
    public class ApartmentService : IApartmentService
    {
        IApartmentRepository _apartmentRepository;
        IUnitOfWork _unitOfWork;
        public ApartmentService(IApartmentRepository apartmentRepository, IUnitOfWork unitOfWork)
        {
            this._apartmentRepository = apartmentRepository;
            this._unitOfWork = unitOfWork;
        }
        public void Add(Apartment apartment)
        {
            _apartmentRepository.Add(apartment);
        }

        public Apartment Delete(int id)
        {
            return _apartmentRepository.Delete(id);
        }

        //public IEnumerable<Apartment> GetAll()
        //{
        //    return _apartmentRepository.GetAll(new string[] { "Apartment" });
        //}
        public IEnumerable<Apartment> GetAll()
        {
            return _apartmentRepository.GetAll();
        }

        public IEnumerable<Apartment> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _apartmentRepository.GetMulti(x => x.ApartmentName.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _apartmentRepository.GetAll();
        }

        public IEnumerable<Apartment> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            //return _apartmentRepository.GetMultiPaging(x=>x.APART_ID == apartmentId, out totalRow, page, pageSize, new string[] { "Apartment" });
            return _apartmentRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Apartment GetById(int id)
        {
            return _apartmentRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Apartment apartment)
        {
            _apartmentRepository.Update(apartment);
        }

        
    }
}
