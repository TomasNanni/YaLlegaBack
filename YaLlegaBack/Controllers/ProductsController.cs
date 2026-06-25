using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

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
            try
            {
                IEnumerable<ProductDataDto> products = _productService.GetAll();
                if (products?.Any() != true)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("GetOneByid/{id}")]
        public IActionResult GetOneById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            try
            {
                ProductDataDto? product = _productService.GetById(id);

                if (product is null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpPost("Create")]
        [Authorize]
        public IActionResult Create([FromBody] NewUpdatedProductDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Debe proporcionar datos válidos del producto.");
            }

            try
            {
                var productId = _productService.Create(dto);
                if (productId == null || productId <= 0)
                {
                    return BadRequest("Ya existe un producto con ese nombre.");
                }

                var product = _productService.GetById((int)productId);
                return Created("Producto Creado: ", product);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("Update/{productId}")]
        [Authorize]
        public IActionResult Update(NewUpdatedProductDto updatedProduct, int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("El ID del producto debe ser mayor a 0.");
            }
            if (updatedProduct == null)
            {
                return BadRequest("Debe proporcionar datos válidos del producto.");
            }

            try
            {
                ServiceResult result = _productService.Update(updatedProduct, productId);
                return StatusCode(result.StatusCode, result.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID del producto debe ser mayor a 0.");
            }

            try
            {
                ServiceResult message = _productService.Delete(id);
                return StatusCode(message.StatusCode, message.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
