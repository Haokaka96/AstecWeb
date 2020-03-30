using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Data.Infastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private AstecDbContext dbContext;

        public AstecDbContext Init()
        {
            return dbContext ?? (dbContext = new AstecDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
