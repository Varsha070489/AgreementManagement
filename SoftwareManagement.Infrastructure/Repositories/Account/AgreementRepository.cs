using SoftwareManagement.Domain.Entities;
using SoftwareManagement.Domain.Interfaces.Account;
using SoftwareManagement.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Repositories.Account
{

    public class AgreementRepository : RepositoryAsync<Agreement>, IAgreementRepository
    {
        public AgreementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }

    public class ProductRespository : RepositoryAsync<Product>, IProductRespository
    {
        public ProductRespository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }

    public class ProductGroupRespository : RepositoryAsync<ProductGroup>, IProductGroupRespository
    {
        public ProductGroupRespository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
