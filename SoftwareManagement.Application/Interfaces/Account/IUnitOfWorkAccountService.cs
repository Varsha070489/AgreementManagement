
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoftwareManagement.Application.Interfaces.Account
{
    public interface IUnitOfWorkAccountService
    {
        IAccountService UserManagementServices { get; }
        IRoleService RoleServices { get; }
        IAgreementService AgreementService { get; }
        IProductGroupService ProductGroupService { get; }
        IProductService ProductService { get; }
    }
}
