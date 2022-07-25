using AutoMapper;
using SoftwareManagement.Application.Interfaces.Account;
using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Application.Services.Account;
using SoftwareManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.Services.Account
{
    public class UnitOfWorkAccountService : IUnitOfWorkAccountService
    {
        public IAccountService UserManagementServices { get; private set; }
        public IRoleService _roleService { get; private set; }

        public IAgreementService _agreementService;
        public IProductGroupService _productGroupService;
        public IProductService _productService;



        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public UnitOfWorkAccountService(IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
            UserManagementServices = new AccountService(_unitOfWork, _mapper);
        }

        public IRoleService RoleServices
        {
            get
            {
                if (_roleService == null)
                {
                    _roleService = new RoleService(_unitOfWork, _mapper, _authenticatedUserService);
                }
                return _roleService;
            }
        }

        public IAgreementService AgreementService
        {
            get
            {
                if (_agreementService == null)
                {
                    _agreementService = new AgreementService(_unitOfWork, _mapper, _authenticatedUserService);
                }
                return _agreementService;
            }
        }

        public IProductGroupService ProductGroupService
        {
            get
            {
                if (_productGroupService == null)
                {
                    _productGroupService = new ProductGroupService(_unitOfWork, _mapper, _authenticatedUserService);
                }
                return _productGroupService;
            }
        }

        public IProductService ProductService
        {
            get
            {
                if (_productService == null)
                {
                    _productService = new ProductService(_unitOfWork, _mapper, _authenticatedUserService);
                }
                return _productService;
            }
        }

    }
}
