using SoftwareManagement.Domain.Interfaces.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWorkForAccount Account { get; }
      
      
        Task<int> Commit();
        Task Rollback();
    }
}
