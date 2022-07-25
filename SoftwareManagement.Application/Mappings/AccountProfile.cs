using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Domain.Entities;
using SoftwareManagement.Domain.Entities.Account;

namespace SoftwareManagement.Application.Mappings
{
    internal class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<AccountResponse, AuthenticateResponse>().ReverseMap();
            CreateMap<Account, AuthenticateResponse>().ReverseMap();
            CreateMap<IQueryable<Account>, IQueryable<AccountResponse>>().ReverseMap();
            CreateMap<RegisterRequest, Account>().ReverseMap();
            CreateMap<RegisterRequest, CreateRequest>().ReverseMap();
            CreateMap<CreateRequest, Account>().ReverseMap();
            CreateMap<UpdateRequest, Account>().ReverseMap();

            CreateMap<RoleCreateRequest, Role>().ReverseMap();
            CreateMap<RoleDeleteRequest, Role>().ReverseMap();
            CreateMap<Role, RoleResponse>().ReverseMap();



            CreateMap<AgreementCreateRequest, Agreement>().ReverseMap();
            CreateMap<AgreementDeleteRequest, Agreement>().ReverseMap();
            CreateMap<Agreement, AgreementResponse>().ReverseMap();

            CreateMap<ProductCreateRequest, Product>().ReverseMap();
            CreateMap<ProductDeleteRequest, Product>().ReverseMap();
            CreateMap<ProductResponse, Product>().ReverseMap();

            CreateMap<ProductGroupCreateRequest, ProductGroup>().ReverseMap();
            CreateMap<ProductGroupDeleteRequest, ProductGroup>().ReverseMap();
            CreateMap<ProductGroupResponse, ProductGroup>().ReverseMap();
           

        }
    }
}
