using Astec.Common.ViewModels;
using Astec.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Service
{
    public interface IStatisticService
    {
        IEnumerable<EmployeeStatisticViewModel> GetEmployeeStatistic(string fromDate, string toDate);
    }
    public class StatisticService : IStatisticService
    {
        IEmployeeRepository _employeeRepository;
        public StatisticService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IEnumerable<EmployeeStatisticViewModel> GetEmployeeStatistic(string fromDate, string toDate)
        {
            return _employeeRepository.GetEmployeeStatistic(fromDate, toDate).OrderBy(e=>e.DateOfBirth);
        }
    }
}
