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
    public interface IStatisticRepository : IRepository<Employee>
    {
        IEnumerable<EmployeeStatisticViewModel> GetEmployeeStatistic(string fromDate, string toDate);
    }
    public class StatisticRepository : RepositoryBase<Employee>, IStatisticRepository
    {
        public StatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<EmployeeStatisticViewModel> GetEmployeeStatistic(string fromDate, string toDate)
        {
            var parameters = new object[]
            {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate",toDate)
            };

            return DbContext.Database.SqlQuery<EmployeeStatisticViewModel>("GetEmployeeStatistic @fromDate,@toDate", parameters);
        }

    }
}
