using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public CategoriesController (ICategoryService categoryService)
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
                return Created("Producto Creado: ", category);
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

            ServiceResult category= _categoryService.GetById(id);

            return StatusCode(category.StatusCode, category.Message);
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public IActionResult Update(UpdatedCategoryDto updatedCategory, int id)
        {
            ServiceResult result = _categoryService.Update(updatedCategory, id);
            return StatusCode(result.StatusCode, result.Message);
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
    }
}
