using Microsoft.AspNetCore.Mvc;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Controllers
{
    [Route("api/Carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
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
                GetCartByIdDto? cart = _cartService.GetById(id);

                if (cart is null)
                {
                    return NotFound();
                }

                return Ok(cart.Products);
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

        [HttpPost("Create/{productId}")]
        public IActionResult Create(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("El id del producto debe ser mayor a 0");
            }

            try
            {
                var result = _cartService.Create(productId);
                if (result == null || result <= 0)
                {
                    return BadRequest("No se pudo crear el carrito");
                }
                return Created($"/cart/{result}", new { Message = $"El id del carrito creado es {result}" });
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

        [HttpPatch("AddProducts/{cartId}/{productId}")]
        public IActionResult AddProduct(int cartId, int productId)
        {
            if (cartId <= 0)
            {
                return BadRequest("El id del carrito debe ser mayor a 0.");
            }
            if (productId <= 0)
            {
                return BadRequest("El id del producto debe ser mayor a 0.");
            }

            try
            {
                ServiceResult result = _cartService.AddProduct(productId, cartId);
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

        [HttpPatch("DeleteProducts/{cartId}/{productId}")]
        public IActionResult DeleteProduct(int cartId, int productId)
        {
            if (cartId <= 0)
            {
                return BadRequest("El id del carrito debe ser mayor a 0.");
            }
            if (productId <= 0)
            {
                return BadRequest("El id del producto debe ser mayor a 0.");
            }

            try
            {
                ServiceResult result = _cartService.DeleteProduct(productId, cartId);
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

        [HttpDelete("Delete/{cartId}")]
        public IActionResult Delete(int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("El id del carrito debe ser mayor a 0.");
            }

            try
            {
                ServiceResult result = _cartService.Delete(cartId);
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
    }
}
