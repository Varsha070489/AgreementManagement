using SoftwareManagement.Domain.Entities.Account;
using SoftwareManagement.Domain.Interfaces.Account;
using SoftwareManagement.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Repositories.Account
{
    public class RoleRepository : RepositoryAsync<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

    }
}
