using AspNetCoreHero.Results;
using AutoMapper;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Application.Interfaces.Account;
using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Domain.Entities.Account;
using SoftwareManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.Services.Account
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public RoleService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
        }

        public IEnumerable<RoleResponse> Roles
        {
            get
            {
                var result = _unitOfWork.Account.Roles.Entities;
                return _mapper.Map<IEnumerable<RoleResponse>>(result);
            }
        }
        public async Task<Result<int>> DeleteAsync(RoleDeleteRequest RoleDeleteRequest)
        {
            var Roles = await _unitOfWork.Account.Roles.GetByIdAsync(RoleDeleteRequest.Id);
            Roles.IsDeleted = true;
            await _unitOfWork.Account.Roles.UpdateAsync(Roles);
            await _unitOfWork.Commit();
            return Result<int>.Success(RoleDeleteRequest.Id);
        }

        public async Task<Result<RoleResponse>> GetByIdAsync(int RoleId)
        {
            var Roles = await _unitOfWork.Account.Roles.GetByIdAsync(RoleId);
            RoleResponse RoleResponse = _mapper.Map<RoleResponse>(Roles);
            return Result<RoleResponse>.Success(RoleResponse);
        }

        public async Task<Result<List<RoleResponse>>> GetListAsync()
        {
            var RoleList = await _unitOfWork.Account.Roles.GetAllAsync();
            RoleList = RoleList.OrderBy(a => a.Name).ToList();
            List<RoleResponse> RoleResponses = _mapper.Map<List<RoleResponse>>(RoleList);
            return Result<List<RoleResponse>>.Success(RoleResponses);
        }

        public async Task<Result<RoleResponse>> InsertAsync(RoleCreateRequest RoleCreateRequest)
        {
            RoleResponse RoleResponse = new RoleResponse();
            var roles = _mapper.Map<Role>(RoleCreateRequest);
            var result = await _unitOfWork.Account.Roles.AddAsync(roles);
            await _unitOfWork.Commit();
            RoleResponse = _mapper.Map<RoleResponse>(result);
            return Result<RoleResponse>.Success(RoleResponse);
        }

        public async Task<Result<int>> UpdateAsync(RoleCreateRequest RoleCreateRequest)
        {
            var Role = await _unitOfWork.Account.Roles.GetByIdAsync(RoleCreateRequest.Id);
            var mappedRole = _mapper.Map(RoleCreateRequest, Role);
            await _unitOfWork.Account.Roles.UpdateAsync(mappedRole);
            await _unitOfWork.Commit();
            return Result<int>.Success(RoleCreateRequest.Id);
        }

    }
}
