using AutoMapper;
using FluentValidation;
using SoftwareManagement.Application.Services.Account;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SoftwareManagement.Application.Interfaces.Account;

using System.Reflection;


namespace SoftwareManagement.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static void AddApplicationRepositories(this IServiceCollection services)
        {
            #region Repositories
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUnitOfWorkAccountService, UnitOfWorkAccountService>();
          
            #endregion Repositories
        }

        
    }
}
