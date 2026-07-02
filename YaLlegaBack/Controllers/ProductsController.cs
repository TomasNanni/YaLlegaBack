using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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
            if (dto.categoriesId == null || dto.categoriesId.Count == 0)
            {
                return BadRequest("Debe indicar al menos una categoría para el producto.");
            }
            if (!int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int userId) || userId <= 0)
            {
                return Unauthorized("Usuario no validado.");
            }
            foreach (var categoryId in dto.categoriesId)
            {
                int? ownerId = _categoryService.GetOwnerId(categoryId);
                if (ownerId == null)
                {
                    return BadRequest($"No existe categoría con id {categoryId}.");
                }
                if (ownerId != userId)
                {
                    return Forbid();
                }
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
            if (!int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int userId) || userId <= 0)
            {
                return Unauthorized("Usuario no validado.");
            }

            try
            {
                List<int>? categoryIds = _productService.GetCategoryIds(productId);
                if (categoryIds == null)
                {
                    return NotFound();
                }
                if (categoryIds.Count == 0 || categoryIds.Any(categoryId => _categoryService.GetOwnerId(categoryId) != userId))
                {
                    return Forbid();
                }
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
            if (!int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int userId) || userId <= 0)
            {
                return Unauthorized("Usuario no validado.");
            }

            try
            {
                List<int>? categoryIds = _productService.GetCategoryIds(id);
                if (categoryIds == null)
                {
                    return NotFound();
                }
                if (categoryIds.Count == 0 || categoryIds.Any(categoryId => _categoryService.GetOwnerId(categoryId) != userId))
                {
                    return Forbid();
                }
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
