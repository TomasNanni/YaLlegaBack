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
            var categoryId = _categoryService.Create(dto, dto.productsId);
            if (categoryId == null)
            {
                return BadRequest("Ya existe una categoria con ese nombre.");
            }
            else
            {
                var category = _categoryService.GetById((int)categoryId);
                return Created("Categoria creada: ", category);
            }
        }
        [HttpGet("GetOneByid/{id}")]
        public IActionResult GetOneById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            GetCategoryById? category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public IActionResult Update(UpdatedCategoryDto updatedCategory, int id)
        {
            GetCategoryById? result = _categoryService.Update(updatedCategory, id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            ServiceResult result = _categoryService.Delete(id);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("AddProduct")]
        [Authorize]
        public IActionResult AddProduct(List<int> productsId, int categoryId)
        {
            ServiceResult result = _categoryService.AddProduct(categoryId, productsId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("RemoveProduct")]
        [Authorize]
        public IActionResult RemoveProduct(List<int> productsId, int categoryId)
        {
            ServiceResult result = _categoryService.RemoveProduct(categoryId, productsId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpGet("GetRestaurantCategories/{restaurantId}")]
        public IActionResult GetRestaurantCategories (int restaurantId)
        {
            if (restaurantId <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

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
    }
}
