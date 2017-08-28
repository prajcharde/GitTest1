using Assignment7.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Repository.Infrastructure
{
    public class IUnitOfWork : IUnitofWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private Employeecontext dataContext;
        public IUnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }
        protected Employeecontext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }
        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
