using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Repository.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        Employeecontext Get();
    }
}
