using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YaLlega.Entities;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;
using YaLlegaBack.Services;


namespace YaLlegaBack.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("Create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody] NewCategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Debe proporcionar datos válidos de la categoría.");
            }

            try
            {
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

            try
            {
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

            try
            {
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
        [HttpPut("AddProduct")]
        [Authorize]
        public IActionResult AddProduct([FromBody] List<int> productsId, int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("El ID de la categoría debe ser mayor a 0.");
            }
            if (productsId == null || productsId.Count == 0)
            {
                return BadRequest("Debe ingresar minimo un producto que agregar.");
            }
            if (productsId.Any(id => id <= 0))
            {
                return BadRequest("Todos los ids de productos deben ser mayores a 0.");
            }

            try
            {
                ServiceResult result = _categoryService.AddProduct(categoryId, productsId);
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
        [HttpPut("RemoveProduct")]
        [Authorize]
        public IActionResult RemoveProduct([FromBody] List<int> productsId, int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("El ID de la categoría debe ser mayor a 0.");
            }
            if (productsId == null || productsId.Count == 0)
            {
                return BadRequest("Debe ingresar minimo un producto que quitar.");
            }
            if (productsId.Any(id => id <= 0))
            {
                return BadRequest("Todos los ids de productos deben ser mayores a 0.");
            }

            try
            {
                ServiceResult result = _categoryService.RemoveProduct(categoryId, productsId);
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
