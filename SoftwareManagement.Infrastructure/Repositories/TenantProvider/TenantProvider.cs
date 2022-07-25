
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Domain.Entities.Tenants;
using SoftwareManagement.Domain.Interfaces.Tenants;
using SoftwareManagement.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Repositories.TenantProvider
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private Tenant _tenant;
        private TenantDbContext _tenantDbContext;
        private string host = "";
        public TenantProvider(TenantDbContext tenantDbContext,
            IAuthenticatedUserService authenticatedUserService)
        {
            _authenticatedUserService = authenticatedUserService;
            _tenantDbContext = tenantDbContext;
            if (!string.IsNullOrEmpty(_authenticatedUserService.HostIdentifier))
            {
                if (_tenant == null)
                {
                    host = _authenticatedUserService.HostIdentifier.Split("@")[1];
                }
                var tenant = _tenantDbContext.Tenants.FirstOrDefault(t => t.Host.ToLower() == host.ToLower());
                if (tenant != null)
                {
                    _tenant = tenant;
                }
            }
        }
        public Tenant GetTenant()
        {
            return _tenant;
        }
    }
}
