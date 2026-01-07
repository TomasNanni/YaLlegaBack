using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YaLlega.Entities;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;
using YaLlegaBack.Services;

namespace YaLlegaBack.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public ActionResult<ProductDataDto> GetAll()
        {
            IEnumerable<ProductDataDto> products = _productService.GetAll();
            if (products?.Any() != true)
            {
                return NoContent();
            }
            else
            {
                return Ok(products);
            }
        }

        [HttpGet("GetOneByid/{id}")]
        [Authorize]
        public IActionResult GetOneById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            ProductDataDto? product = _productService.GetById(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpPost("Create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody] NewUpdatedProductDto dto)
        {
            var productId = _productService.Create(dto);
            if (productId == null)
            {
                return BadRequest("Ya existe un producto con ese nombre.");
            }
            else
            {
                var product = _productService.GetById((int)productId);
                return Created("Producto Creado: ",product);
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update(NewUpdatedProductDto updatedProduct, int productId)
        {
            ServiceResult result = _productService.Update(updatedProduct, productId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            ServiceResult message = _productService.Delete(id);
            return StatusCode(message.StatusCode, message.Message);
        }
    }
}
