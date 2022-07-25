
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Application.Interfaces.Account;
using SoftwareManagement.Domain.Interfaces;

namespace SoftwareManagement.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<AccountResponse> Accounts => _mapper.Map<IEnumerable<AccountResponse>>(_unitOfWork.Account.Accounts.Entities);

        public async Task<AccountResponse> GetByIdAsync(int accountId)
        {
            var account = await _unitOfWork.Account.Accounts.GetByIdAsync(accountId);
            AccountResponse accountResponse = _mapper.Map<AccountResponse>(account);
            return accountResponse;
        }

        public async Task<List<AccountResponse>> GetListAsync()
        {
            var accountList = await _unitOfWork.Account.Accounts.GetAllAsync();
            List<AccountResponse> accountResponses = _mapper.Map<List<AccountResponse>>(accountList);
            return accountResponses;
        }

        public async Task<AccountResponse> InsertAsync(CreateRequest createRequest)
        {
            AccountResponse accountResponse = new AccountResponse();
            var account = _mapper.Map<Domain.Entities.Account.Account>(createRequest);
            var result = await _unitOfWork.Account.Accounts.AddAsync(account);
            await _unitOfWork.Commit();
            accountResponse = _mapper.Map<AccountResponse>(result);
            return accountResponse;
        }

        public async Task UpdateAsync(UpdateRequest updateRequest)
        {
            var account = _mapper.Map<Domain.Entities.Account.Account>(updateRequest);
            await _unitOfWork.Account.Accounts.UpdateAsync(account);
        }

        public async Task<AccountResponse> GetByEmailId(string emailId)
        {
            AccountResponse accountResponse = null;
            await Task.Run(() =>
            {
                var account = _unitOfWork.Account.Accounts.Entities.FirstOrDefault(a => a.Email.ToLower() == emailId.ToLower());
                accountResponse = _mapper.Map<AccountResponse>(account);
            });
            return accountResponse;
        }
    }
}
