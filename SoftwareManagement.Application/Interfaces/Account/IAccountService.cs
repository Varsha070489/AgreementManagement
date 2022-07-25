
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Application.DTOs.Request.Account;

namespace SoftwareManagement.Application.Interfaces.Account
{
    public interface IAccountService
    {
        IEnumerable<AccountResponse> Accounts { get; }

        Task<List<AccountResponse>> GetListAsync();

        Task<AccountResponse> GetByIdAsync(int accountId);

        Task<AccountResponse> GetByEmailId(string emailId);

        Task<AccountResponse> InsertAsync(CreateRequest createRequest);

        Task UpdateAsync(UpdateRequest updateRequest);
    }
}
