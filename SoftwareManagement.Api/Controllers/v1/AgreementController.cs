using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareManagement.Application.Interfaces.Account;
using Microsoft.AspNetCore.Http;
using SoftwareManagement.Application.DTOs.Response.Account;
using AspNetCoreHero.Results;
using SoftwareManagement.Application.DTOs.Request.DatatableModel;
using SoftwareManagement.Application.DTOs.Request.Account;

namespace SoftwareManagement.Api.Controllers.v1
{
    public class AgreementController : BaseApiController<AgreementController>
    {
        private readonly IUnitOfWorkAccountService _agreementService;

        public AgreementController(IUnitOfWorkAccountService unitOfWorkAccountService)
        {
            _agreementService = unitOfWorkAccountService;
        }

        /// <summary>
        /// Get all agreements
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("get-all-agreements")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<AgreementResponse>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _agreementService.AgreementService.GetListAsync();
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

        /// <summary>
        /// Get all agreement with parameters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-agreementsIncludeParameter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<AgreementResponse>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDts()
        {
            var response = await _agreementService.AgreementService.GetListAsync(new string[] { "ProductGroup", "Product" });
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

       

        /// <summary>
        /// Get agreement by agreement id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<AgreementResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _agreementService.AgreementService.GetByIdAsync(id);
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

        /// <summary>
        /// Save agreement
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-agreement")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<AgreementResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] AgreementCreateRequest createRequest)
        {
            var response = await _agreementService.AgreementService.InsertAsync(createRequest);
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

        /// <summary>
        /// Update agreement
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-agreement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<AgreementResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] AgreementCreateRequest createRequest)
        {
            var response = await _agreementService.AgreementService.UpdateAsync(createRequest);
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

        /// <summary>
        /// Delete agreement
        /// </summary>
        /// <param name="deleteRequest"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("delete-agreement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<int>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromBody] AgreementDeleteRequest deleteRequest)
        {
            var response = await _agreementService.AgreementService.DeleteAsync(deleteRequest);
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

        #region Product 

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("get-all-products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<ProductResponse>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await _agreementService.ProductService.GetListAsync();
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

        /// <summary>
        /// Get product by product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-ProductById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _agreementService.ProductService.GetByIdAsync(id);
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }
        #endregion

        #region Product groups

        /// <summary>
        /// Get all product groups
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("get-all-productGroups")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<ProductGroupResponse>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProductGroup()
        {
            var response = await _agreementService.ProductGroupService.GetListAsync();
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound();
        }

       
        #endregion
    }
}
