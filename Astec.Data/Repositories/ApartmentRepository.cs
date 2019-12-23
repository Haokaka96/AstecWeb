using Astec.Data.Infastructure;
using Astec.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Astec.Data.Repositories
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        IEnumerable<Apartment> GetListApartment(int page, int pageSize, out int totalRow);
    }

    public class ApartmentRepository : RepositoryBase<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Apartment> GetListApartment(int page, int pageSize, out int totalRow)
        {
            var query = DbContext.Apartments.ToList();
            totalRow = query.Count();
            return query.OrderBy(a => a.ApartmentID).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}