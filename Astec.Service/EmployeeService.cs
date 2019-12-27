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
    public interface IEmployeeService
    {
        void Add(Employee employee);
        void Update(Employee employee);
        // void Delete(int id);
        Employee Delete(int id);
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetListEmloyee(string filter);
        IEnumerable<Employee> GetAll(int page, int pageSize, out int totalRow, string filter = null);
        Employee GetById(int id);
        void SaveChanges();
    }
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IUnitOfWork _unitOfWork;
        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }
        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        public Employee Delete(int id)
        {
            return _employeeRepository.Delete(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        //public IEnumerable<Employee> GetAll(string filter)
        //{
        //    if (!string.IsNullOrEmpty(filter))
        //        return _employeeRepository.GetMulti(x => x.Name.Contains(filter));
        //    else
        //        return _employeeRepository.GetAll();
        //}

        public IEnumerable<Employee> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _employeeRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetSingleById(id);
        }

        public IEnumerable<Employee> GetListEmloyee(string filter)
        {
            IEnumerable<Employee> query;
            if (!string.IsNullOrEmpty(filter))
                query = _employeeRepository.GetMulti(x => x.Name.Contains(filter));
            else
                query = _employeeRepository.GetAll();
            return query;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
        }
    }
}
