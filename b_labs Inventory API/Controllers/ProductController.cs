using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace b_labs_Inventory_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductCore _productCore;
        public ProductController(IProductCore productCore)
        {
            _productCore = productCore;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductDTO request, [FromHeader] string? langauage)
        {
            var dtoResponse = await _productCore.GetAllProducts(request, langauage);
            return StatusCode((int)dtoResponse.StatusCode, dtoResponse.Data);
        }


        [HttpGet]
        [Route("{productKey}")]
        public async Task<IActionResult> GetByKey(string productKey, [FromHeader] string? langauage)
        {
            var dtoResponse = await _productCore.GetProductByKey(productKey, langauage);
            return StatusCode((int)dtoResponse.StatusCode, dtoResponse.Data);
        }

        [HttpDelete]
        [Route("{productKey}")]
        public IActionResult Delete(string productKey)
        {
            var dtoResponse = _productCore.DeleteProduct(productKey);
            return StatusCode((int)dtoResponse.StatusCode, dtoResponse.Data);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] AddProductDTO dto)
        {
            var dtoResponse = _productCore.AddProduct(dto);
            return StatusCode((int)dtoResponse.StatusCode, dtoResponse.Data);
        }
        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] UpdateProductDTO request)
        {
            var dtoResponse = _productCore.UpdateProduct(request);
            return StatusCode((int)dtoResponse.StatusCode, dtoResponse.Data);
        }

        [HttpPost]
        [Route("Rate/{productKey}")]
        public IActionResult Rate(string productKey, [Range(1, 5)] int rate)
        {
            var dtoResponse = _productCore.RateProduct(productKey, rate);
            return StatusCode((int)dtoResponse.StatusCode, dtoResponse.Data);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchProductDTO request, [FromHeader] string? langauage)
        {
            var dtoResponse = await _productCore.Search(request, langauage);
            return StatusCode((int)dtoResponse.StatusCode, dtoResponse.Data);
        }
    }
}
