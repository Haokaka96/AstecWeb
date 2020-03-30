using Astec.Data.Infastructure;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Repositories
{
    public interface IResidentRepository:IRepository<Resident>
    {
        IEnumerable<Resident> GetListResidents(int page, int pageSize, out int totalRow);
    }
    public class ResidentRepository: RepositoryBase<Resident>,IResidentRepository
    {
        public ResidentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Resident> GetListResidents(int page, int pageSize, out int totalRow)
        {
            var query = DbContext.Residents.ToList();
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
