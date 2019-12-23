using Astec.Data.Infastructure;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Repositories
{
    public interface IApplicationModuleRepository : IRepository<ApplicationModule>
    {
        IEnumerable<ApplicationModule> GetListModule();
    }
    public class ApplicationModuleRepository: RepositoryBase<ApplicationModule>, IApplicationModuleRepository
    {
        public ApplicationModuleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationModule> GetListModule()
        {
            var query = DbContext.ApplicationModules.ToList();
            return query;
        }
    }
}
