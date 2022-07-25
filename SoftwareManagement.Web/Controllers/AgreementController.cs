using AspNetCoreHero.Results;
using SoftwareManagement.Application.DTOs.Request.DatatableModel;
using SoftwareManagement.Web.Abstractions;
using SoftwareManagement.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Response.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SoftwareManagement.Web.Models;

namespace SoftwareManagement.Web.Controllers
{
    public class AgreementController : BaseController<AgreementController>
    {
       
        public AgreementController()
        {
           
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync<Result<List<AgreementResponse>>>("api/v1/Agreement/get-all-agreementsIncludeParameter");
            var list = new List<AgreementResponse>();
            if (response != null && response.Succeeded)
            {
                list = response.Data;
                return View(list);
            }
            return View(new List<AgreementResponse>());
        }
        public async Task<IActionResult> Agreement()
        {
            AgreementCreateRequest obj = new AgreementCreateRequest();
            obj.EffectiveDate = System.DateTime.Now;
            obj.ExpirationDate = System.DateTime.Now;
            obj.ProductGroupId = 0;
            obj.ProductId = 0;
            return View(obj);
        }

        public async Task<IActionResult> Edit(int id)
        {
            
            AgreementCreateRequest obj = new AgreementCreateRequest();

            var response = await _httpClient.GetAsync<Result<AgreementResponse>>("api/v1/Agreement/" + id);
            obj.Id = id;
            obj.NewPrice = response.Data.NewPrice;
            obj.ProductPrice = response.Data.ProductPrice;
            obj.UserId = response.Data.UserId;
            obj.ProductId = response.Data.ProductId;
            obj.ProductGroupId = response.Data.ProductGroupId;
            obj.EffectiveDate = Convert.ToDateTime(response.Data.EffectiveDate);
            obj.ExpirationDate = Convert.ToDateTime(response.Data.ExpirationDate);
            obj.IsActive = response.Data.IsActive;
            GetProduct(obj.ProductGroupId);
            return View("Agreement", obj);
        }

     

        public async Task<IActionResult> CreateAgreement(AgreementCreateRequest createRequest)
        {
            //ViewBag.UserId = HttpContext.Session.Id;
            createRequest.UserId = 1;
            var stringContent = Utility.GetContent(createRequest);
            if (createRequest.Id != 0)
            {
                var response = await _httpClient.PostAsync<Result<AgreementResponse>>("api/v1/Agreement/update-agreement", stringContent);
                if (response.Failed)
                    _notify.Error(ResourceHelper.GetResourceValue("msg.DuplicateMsg"));
                else
                    _notify.Success(ResourceHelper.GetResourceValue("msg.RecordUpdate"));
            }
            else
            {
                var response = await _httpClient.PostAsync<Result<AgreementResponse>>("api/v1/Agreement/create-agreement", stringContent);
                if (response.Failed)
                {
                    _notify.Error(ResourceHelper.GetResourceValue("msg.DuplicateMsg"));
                }
                else
                { _notify.Success(ResourceHelper.GetResourceValue("msg.RecordSave")); }
            }
            return RedirectToAction("Index", "Agreement");
        }


        public async Task<IActionResult> DeleteAgreement(int Id)
        {
            AgreementDeleteRequest obj = new AgreementDeleteRequest();
            obj.Id = Id;
            var stringContent = Utility.GetContent(obj);
            var response = await _httpClient.PostAsync<Result<int>>("api/v1/Agreement/delete-agreement", stringContent);
            if (response.Succeeded)
            {
                _notify.Success(ResourceHelper.GetResourceValue("msg.RecordDeleted"));
                return Ok(response.Data);
            }
            return Ok();
        }




        public SelectList GetProduct(int groupId)
        {
            var locations = _httpClient.GetAsync<Result<List<ProductResponse>>>("api/v1/Agreement/get-all-products").Result.Data;

            if (groupId != 0)
            {
                var list = locations.Where(a => a.ProductGroupId == groupId);
                if (locations != null)
                {
                    var customerList = new SelectList(list, "Id", "ProductDescription");
                    return customerList;
                }
            }
            else
            {
                var list = locations;
                if (locations != null)
                {
                    var customerList = new SelectList(list, "Id", "ProductDescription");
                    return customerList;
                }
            }


            return null;
        }

        public SelectList GetProductGroup()
        {
            var locations = _httpClient.GetAsync<Result<List<ProductGroupResponse>>>("api/v1/Agreement/get-all-productGroups").Result.Data;
            if (locations != null)
            {
                var customerList = new SelectList(locations, "Id", "Description");
                return customerList;
            }
            return null;
        }

        public async Task<IActionResult> GetProductPrice(int productId)
        {
            var list = await _httpClient.GetAsync<Result<ProductResponse>>("api/v1/Agreement/get-ProductById?id=" + productId);
            var price = list.Data.Price;
            return Json(price, new Newtonsoft.Json.JsonSerializerSettings());

        }
    }
}
