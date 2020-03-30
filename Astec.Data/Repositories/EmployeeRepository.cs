using Astec.Common.ViewModels;
using Astec.Data.Infastructure;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Repositories
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        IEnumerable<Employee> GetListEmployeeByName(string name);
        IEnumerable<EmployeeStatisticViewModel> GetEmployeeStatistic(string fromDate, string toDate);
    }
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<EmployeeStatisticViewModel> GetEmployeeStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate",toDate)
            };

            return DbContext.Database.SqlQuery<EmployeeStatisticViewModel>("GetEmployeeStatistic @fromDate,@toDate", parameters);
        }

        public IEnumerable<Employee> GetListEmployeeByName(string name)
        {
            return this.DbContext.Employees.Where(x => x.Name == name);
        }
    }
}
