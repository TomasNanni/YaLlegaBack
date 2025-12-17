using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;
using YaLlegaBack.Services;

namespace YaLlegaBack.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("GetOneByid/{id}")]
        [Authorize]
        public IActionResult GetOneById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            GetCartByIdDto? cart = _cartService.GetById(id);

            if (cart is null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("Create")]
        [Authorize]
        public IActionResult Create([FromBody] List<NewUpdatedProductDto> dto)
        {
            if (dto == null)
            {
                return BadRequest("Debe introducir minimo un producto para agregar");
            }
            var result = _cartService.Create(dto);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Created($"/cart/{result}", new { Message = $"El id del carrito creado es {result}" });

            }
        }

        [HttpPatch("AddProducts/{cartId}")]
        [Authorize]
        public IActionResult AddProduct([FromBody] List<NewUpdatedProductDto> products, int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("El id del carrito debe ser mayor a 0.");
            }
            if (products == null)
            {
                return BadRequest("Debe ingresar minimo un producto que agregar.");
            }
            ServiceResult result = _cartService.AddProduct(products, cartId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPatch("DeleteProducts/{cartId}")]
        [Authorize]
        public IActionResult DeleteProduct([FromBody] List<NewUpdatedProductDto> products, int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("El id del carrito debe ser mayor a 0.");
            }
            if (products == null)
            {
                return BadRequest("Debe ingresar minimo un producto que quitar del carrito.");
            }
            ServiceResult result = _cartService.DeleteProduct(products, cartId);
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("Delete/{cartId}")]
        [Authorize]
        public IActionResult Delete(int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("El id del carrito debe ser mayor a 0.");
            }
            ServiceResult result = _cartService.Delete(cartId);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
