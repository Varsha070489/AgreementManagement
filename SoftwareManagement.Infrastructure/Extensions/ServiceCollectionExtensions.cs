using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftwareManagement.Domain.Interfaces;
using SoftwareManagement.Domain.Interfaces.Account;
using SoftwareManagement.Infrastructure.Repositories;
using SoftwareManagement.Infrastructure.Repositories.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            #region [Account/User Module]
            services.AddTransient<IAccountRespository, AccountRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUnitOfWorkForAccount, UnitOfWorkForAccount>();
            #endregion

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion Repositories
        }
    }
}
