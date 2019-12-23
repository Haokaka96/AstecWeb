using Astec.Data.Infastructure;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Repositories
{
    public interface IStreetRepository: IRepository<Street>
    {
        IEnumerable<Street> GetListStreets(int page, int pageSize, out int totalRow);
    }
    public class StreetRepository: RepositoryBase<Street>, IStreetRepository
    {
        public StreetRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<Street> GetListStreets(int page, int pageSize, out int totalRow)
        {
            var query = DbContext.Streets.ToList();
            totalRow = query.Count();
            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
