using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Domain.Interfaces;
using SoftwareManagement.Domain.Interfaces.Account;
using SoftwareManagement.Infrastructure.DbContexts;
using SoftwareManagement.Infrastructure.Repositories.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed;

        public IUnitOfWorkForAccount _account;

        public IAuthenticatedUserService _authenticatedUserService;
     
        public UnitOfWork(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUserService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _authenticatedUserService = authenticatedUserService;
        }


        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync(_authenticatedUserService.UserId);
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
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

        public IUnitOfWorkForAccount Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new UnitOfWorkForAccount(_dbContext);
                }

                return _account;
            }
        }

    }
}