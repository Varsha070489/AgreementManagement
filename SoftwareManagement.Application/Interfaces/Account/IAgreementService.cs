using AspNetCoreHero.Results;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Request.DatatableModel;
using SoftwareManagement.Application.DTOs.Response.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.Interfaces.Account
{
    public interface IAgreementService
    {
        IEnumerable<AgreementResponse> Agreements { get; }
      
        Task<Result<List<AgreementResponse>>> GetListAsync();
        Task<Result<List<AgreementResponse>>> GetListAsync(string[] includeparam);
        Task<Result<AgreementResponse>> GetByIdAsync(int id);
        Task<Result<AgreementResponse>> InsertAsync(AgreementCreateRequest createRequest);
        Task<Result<AgreementResponse>> UpdateAsync(AgreementCreateRequest createRequest);
        Task<Result<int>> DeleteAsync(AgreementDeleteRequest deleteRequest);
    }

    public interface IProductService
    {
        IEnumerable<ProductResponse> Products { get; }
       
        Task<Result<List<ProductResponse>>> GetListAsync();
       
        Task<Result<ProductResponse>> GetByIdAsync(int id);
        Task<Result<ProductResponse>> InsertAsync(ProductCreateRequest createRequest);
        Task<Result<ProductResponse>> UpdateAsync(ProductCreateRequest createRequest);
        Task<Result<int>> DeleteAsync(ProductDeleteRequest deleteRequest);
    }

    public interface IProductGroupService
    {
        IEnumerable<ProductGroupResponse> ProductGroups { get; }
        Task<Result<List<ProductGroupResponse>>> GetListAsync();
     
        Task<Result<ProductGroupResponse>> InsertAsync(ProductGroupCreateRequest createRequest);
        Task<Result<ProductGroupResponse>> UpdateAsync(ProductGroupCreateRequest createRequest);
        Task<Result<int>> DeleteAsync(ProductGroupDeleteRequest deleteRequest);

    }
}
