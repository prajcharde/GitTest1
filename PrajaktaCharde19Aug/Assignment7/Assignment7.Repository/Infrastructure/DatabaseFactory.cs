using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Repository.Infrastructure
{

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private Employeecontext dataContext;
        public Employeecontext Get()
        {
            return dataContext ?? (dataContext = new Employeecontext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
