using Astec.Data.Infastructure;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Repositories
{
    public interface IWardRepository : IRepository<Wards>
    {
        IEnumerable<Wards> GetListWards(int page, int pageSize, out int totalRow);
    }
    public class WardsRepository : RepositoryBase<Wards>, IWardRepository
    {
        public WardsRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public IEnumerable<Wards> GetListWards(int page, int pageSize, out int totalRow)
        {
            var query = DbContext.Wardses.ToList();
            totalRow = query.Count();
            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
