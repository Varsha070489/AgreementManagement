using AspNetCoreHero.Results;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Response.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoftwareManagement.Application.Interfaces.Account
{
   public interface IRoleService
    {
        IEnumerable<RoleResponse> Roles { get; }
        Task<Result<List<RoleResponse>>> GetListAsync();
        Task<Result<RoleResponse>> GetByIdAsync(int RoleId);
        Task<Result<RoleResponse>> InsertAsync(RoleCreateRequest RoleCreateRequest);
        Task<Result<int>> UpdateAsync(RoleCreateRequest RoleCreateRequest);
        Task<Result<int>> DeleteAsync(RoleDeleteRequest RoleDeleteRequest);
    }
}
