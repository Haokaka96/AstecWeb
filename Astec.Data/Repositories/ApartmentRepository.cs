using Astec.Data.Infastructure;
using Astec.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Astec.Data.Repositories
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        IEnumerable<Apartment> GetListApartmentName(string name);
    }

    public class ApartmentRepository : RepositoryBase<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Apartment> GetListApartmentName(string name)
        {
            return this.DbContext.Apartments.Where(x => x.ApartmentName == name);
        }
    }
}