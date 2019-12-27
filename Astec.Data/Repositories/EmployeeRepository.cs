using Astec.Data.Infastructure;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Repositories
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        IEnumerable<Employee> GetListEmployeeByName(string name);
    }
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Employee> GetListEmployeeByName(string name)
        {
            return this.DbContext.Employees.Where(x => x.Name == name);
        }
    }
}
