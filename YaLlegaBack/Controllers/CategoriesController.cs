using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;


namespace YaLlegaBack.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        [HttpPost("Create")]
        [Authorize]
        public IActionResult Create([FromBody] NewCategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Debe proporcionar datos válidos de la categoría.");
            }
            if (!int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int userId) || userId <= 0)
            {
                return Unauthorized("Usuario no validado.");
            }
            if (dto.productsId != null)
            {
                foreach (var productId in dto.productsId)
                {
                    List<int>? existingCategoryIds = _productService.GetCategoryIds(productId);
                    if (existingCategoryIds != null && existingCategoryIds.Any(existingCategoryId => _categoryService.GetOwnerId(existingCategoryId) != userId))
                    {
                        return Forbid();
                    }
                }
            }

            try
            {
                dto.RestaurantUserId = userId;
                var categoryId = _categoryService.Create(dto, dto.productsId);
                if (categoryId == null || categoryId <= 0)
                {
                    return BadRequest("Ya existe una categoria con ese nombre.");
                }

                var category = _categoryService.GetById((int)categoryId);
                return Created("Categoria creada: ", category);
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
        [HttpGet("GetOneByid/{id}")]
        public IActionResult GetOneById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            try
            {
                GetCategoryById? category = _categoryService.GetById(id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
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
        [HttpPut("Update/{id}")]
        [Authorize]
        public IActionResult Update(UpdatedCategoryDto updatedCategory, int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID de la categoría debe ser mayor a 0.");
            }
            if (updatedCategory == null)
            {
                return BadRequest("Debe proporcionar datos válidos de la categoría.");
            }
            if (!int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int userId) || userId <= 0)
            {
                return Unauthorized("Usuario no validado.");
            }

            try
            {
                int? ownerId = _categoryService.GetOwnerId(id);
                if (ownerId == null)
                {
                    return NotFound();
                }
                if (ownerId != userId)
                {
                    return Forbid();
                }
                if (updatedCategory.ProductIds != null)
                {
                    foreach (var productId in updatedCategory.ProductIds)
                    {
                        List<int>? existingCategoryIds = _productService.GetCategoryIds(productId);
                        if (existingCategoryIds != null && existingCategoryIds.Any(existingCategoryId => _categoryService.GetOwnerId(existingCategoryId) != userId))
                        {
                            return Forbid();
                        }
                    }
                }
                GetCategoryById? result = _categoryService.Update(updatedCategory, id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
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
                return BadRequest("El ID de la categoría debe ser mayor a 0.");
            }
            if (!int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int userId) || userId <= 0)
            {
                return Unauthorized("Usuario no validado.");
            }

            try
            {
                int? ownerId = _categoryService.GetOwnerId(id);
                if (ownerId == null)
                {
                    return NotFound();
                }
                if (ownerId != userId)
                {
                    return Forbid();
                }
                ServiceResult result = _categoryService.Delete(id);
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
        [HttpGet("GetRestaurantCategories/{restaurantId}")]
        public IActionResult GetRestaurantCategories(int restaurantId)
        {
            if (restaurantId <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            try
            {
                List<GetCategoryById>? category = _categoryService.GetRestaurantCategories(restaurantId);

                if (category == null)
                {
                    return BadRequest("El restaurante no fue encontrado.");
                }
                if (category.Count() == 0)
                {
                    return NotFound("El restaurante no tiene categorias");
                }

                return Ok(category);
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
