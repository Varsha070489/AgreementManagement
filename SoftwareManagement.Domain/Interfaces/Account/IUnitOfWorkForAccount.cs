using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Interfaces.Account
{
    public interface IUnitOfWorkForAccount : IDisposable
    {
        IAccountRespository Accounts { get; }
        IRoleRepository Roles { get; }

        IAgreementRepository Agreement { get; }
        IProductRespository Product { get; }
        IProductGroupRespository ProductGroup { get; }
    }
}
