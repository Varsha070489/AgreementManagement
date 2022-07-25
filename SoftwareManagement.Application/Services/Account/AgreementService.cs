using AspNetCoreHero.Results;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Request.DatatableModel;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Application.Enums;
using SoftwareManagement.Application.Extensions;
using SoftwareManagement.Application.Interfaces.Account;
using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Domain.Entities;
using SoftwareManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.Services.Account
{

    public class AgreementService : IAgreementService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public AgreementService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
        }

        public IEnumerable<AgreementResponse> Agreements
        {
            get
            {
                var result = _unitOfWork.Account.Agreement.Entities;
                return _mapper.Map<IEnumerable<AgreementResponse>>(result);
            }
        }

   

        public async Task<Result<List<AgreementResponse>>> GetListAsync()
        {
            var list = await _unitOfWork.Account.Agreement.GetAllAsync();
            list = list.OrderByDescending(a => a.Id).ToList();
            List<AgreementResponse> response = _mapper.Map<List<AgreementResponse>>(list);
            return Result<List<AgreementResponse>>.Success(response);
        }

        public async Task<Result<AgreementResponse>> GetByIdAsync(int id)
        {
            var agreement = await _unitOfWork.Account.Agreement.GetByIdAsync(id);
            AgreementResponse response = _mapper.Map<AgreementResponse>(agreement);
            return Result<AgreementResponse>.Success(response);
        }

        public async Task<Result<AgreementResponse>> InsertAsync(AgreementCreateRequest createRequest)
        {
            AgreementResponse response = new AgreementResponse();
            var agreement = _mapper.Map<Agreement>(createRequest);
            var result = await _unitOfWork.Account.Agreement.AddAsync(agreement);
            await _unitOfWork.Commit();
            response = _mapper.Map<AgreementResponse>(result);
            return Result<AgreementResponse>.Success(response);
        }

        public async Task<Result<AgreementResponse>> UpdateAsync(AgreementCreateRequest createRequest)
        {
            var agreement = await _unitOfWork.Account.Agreement.GetByIdAsync(createRequest.Id);
            var mappedAgreement = _mapper.Map(createRequest, agreement);
            await _unitOfWork.Account.Agreement.UpdateAsync(mappedAgreement);
            await _unitOfWork.Commit();
            return Result<AgreementResponse>.Success(new AgreementResponse());
        }

        public async Task<Result<int>> DeleteAsync(AgreementDeleteRequest deleteRequest)
        {
            var agreement = await _unitOfWork.Account.Agreement.GetByIdAsync(deleteRequest.Id);
            agreement.IsDeleted = true;
            await _unitOfWork.Account.Agreement.UpdateAsync(agreement);
            await _unitOfWork.Commit();
            return Result<int>.Success(deleteRequest.Id);
        }

        public async Task<Result<List<AgreementResponse>>> GetListAsync(string[] includeparam)
        { 
            var agreement = await _unitOfWork.Account.Agreement.GetAllAsync(includeparam);
            var list = (from a in agreement
                        join u in _unitOfWork.Account.Accounts.Entities on a.UserId equals u.Id
                         select new AgreementResponse {
                             Id = a.Id,
                             NewPrice = a.NewPrice,
                             ProductGroupId = a.ProductGroupId,
                             ProductId = a.ProductId,
                            UserName = (u.FirstName + ' ' + u.LastName),
                             Group = a.ProductGroup.Description,
                             GroupCode = a.ProductGroup.Code,
                             ProductDescription = a.Product.ProductDescription,
                             ProductNumber = a.Product.ProductNumber,
                             ProductPrice=a.Product.Price,
                             IsActive = a.IsActive,
                             EffectiveDate=a.EffectiveDate,
                             ExpirationDate=a.ExpirationDate,
                             
                         }).ToList();
            List <AgreementResponse> response = _mapper.Map<List<AgreementResponse>>(list);
            return Result<List<AgreementResponse>>.Success(response);
        }
    }

    #region Product
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public ProductService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
        }

        public IEnumerable<ProductResponse> Products
        {
            get
            {
                var result = _unitOfWork.Account.Product.Entities;
                return _mapper.Map<IEnumerable<ProductResponse>>(result);
            }
        }

        public async Task<Result<List<ProductResponse>>> GetListAsync()
        {
            var list = await _unitOfWork.Account.Product.GetAllAsync();
            list = list.OrderByDescending(a => a.Id).ToList();
            List<ProductResponse> response = _mapper.Map<List<ProductResponse>>(list);
            return Result<List<ProductResponse>>.Success(response);
        }

        public async Task<Result<ProductResponse>> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Account.Product.GetByIdAsync(id);
            ProductResponse response = _mapper.Map<ProductResponse>(product);
            return Result<ProductResponse>.Success(response);
        }
    
        public async Task<Result<ProductResponse>> InsertAsync(ProductCreateRequest createRequest)
        {
            ProductResponse response = new ProductResponse();
            var product = _mapper.Map<Product>(createRequest);
            var result = await _unitOfWork.Account.Product.AddAsync(product);
            await _unitOfWork.Commit();
            response = _mapper.Map<ProductResponse>(result);
            return Result<ProductResponse>.Success(response);
        }


        public async Task<Result<ProductResponse>> UpdateAsync(ProductCreateRequest createRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteAsync(ProductDeleteRequest deleteRequest)
        {
            throw new NotImplementedException();
        }


    }
    #endregion

    #region Product Group
    public class ProductGroupService : IProductGroupService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public ProductGroupService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthenticatedUserService authenticatedUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUserService = authenticatedUserService;
        }

        public IEnumerable<ProductGroupResponse> ProductGroups
        {
            get
            {
                var result = _unitOfWork.Account.ProductGroup.Entities;
                return _mapper.Map<IEnumerable<ProductGroupResponse>>(result);
            }
        }

        public async Task<Result<List<ProductGroupResponse>>> GetListAsync()
        {
            var list = await _unitOfWork.Account.ProductGroup.GetAllAsync();
            list = list.OrderByDescending(a => a.Id).ToList();
            List<ProductGroupResponse> response = _mapper.Map<List<ProductGroupResponse>>(list);
            return Result<List<ProductGroupResponse>>.Success(response);
        }

  

        public async Task<Result<ProductGroupResponse>> InsertAsync(ProductGroupCreateRequest createRequest)
        {
            ProductGroupResponse response = new ProductGroupResponse();
            var product = _mapper.Map<ProductGroup>(createRequest);
            var result = await _unitOfWork.Account.ProductGroup.AddAsync(product);
            await _unitOfWork.Commit();
            response = _mapper.Map<ProductGroupResponse>(result);
            return Result<ProductGroupResponse>.Success(response);
        }

        public async Task<Result<ProductGroupResponse>> UpdateAsync(ProductGroupCreateRequest createRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteAsync(ProductGroupDeleteRequest deleteRequest)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
