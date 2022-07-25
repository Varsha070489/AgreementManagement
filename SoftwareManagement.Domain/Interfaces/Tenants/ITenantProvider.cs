using SoftwareManagement.Domain.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Interfaces.Tenants
{
    public interface ITenantProvider
    {
        Tenant GetTenant();
    }
}
