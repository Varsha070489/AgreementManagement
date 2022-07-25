using SoftwareManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Interfaces.Account
{
    public interface IAgreementRepository : IRepositoryAsync<Agreement>
    {
    }
    public interface IProductRespository : IRepositoryAsync<Product>
    {

    }
    public interface IProductGroupRespository : IRepositoryAsync<ProductGroup>
    {

    }
}
