using SoftwareManagement.Domain.Interfaces.Account;
using SoftwareManagement.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Repositories.Account
{
    public class AccountRepository : RepositoryAsync<Domain.Entities.Account.Account>, IAccountRespository
    {
        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
