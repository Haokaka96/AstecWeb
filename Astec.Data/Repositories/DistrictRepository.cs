using Astec.Data.Infastructure;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Repositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        IEnumerable<District> GetListDistrict(int page, int pageSize, out int totalRow);
    }
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(IDbFactory dbFactory): base(dbFactory) { }
        public IEnumerable<District> GetListDistrict(int page, int pageSize, out int totalRow)
        {
            var query = DbContext.Districts.ToList();
            totalRow = query.Count();
            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
