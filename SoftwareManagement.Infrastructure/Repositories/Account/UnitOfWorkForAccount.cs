using SoftwareManagement.Domain.Interfaces.Account;
using SoftwareManagement.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Repositories.Account
{
    public class UnitOfWorkForAccount : IUnitOfWorkForAccount
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed;

        public IAccountRespository _accounts;
        public IRoleRepository _roles;

        public IAgreementRepository _agreement;

        public IProductRespository _product;
        public IProductGroupRespository _productGroup;



        public UnitOfWorkForAccount(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IAccountRespository Accounts
        {
            get
            {
                if (_accounts == null)
                {
                    _accounts = new AccountRepository(_dbContext);
                }

                return _accounts;
            }
        }
        public IRoleRepository Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RoleRepository(_dbContext);
                }

                return _roles;
            }
        }

        public IProductGroupRespository ProductGroup
        {
            get
            {
                if (_productGroup == null)
                {
                    _productGroup = new ProductGroupRespository(_dbContext);
                }

                return _productGroup;
            }
        }

        public IProductRespository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRespository(_dbContext);
                }

                return _product;
            }
        }

        public IAgreementRepository Agreement
        {
            get
            {
                if (_agreement == null)
                {
                    _agreement = new AgreementRepository(_dbContext);
                }

                return _agreement;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

    }
}
