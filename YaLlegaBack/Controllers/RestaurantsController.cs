using Microsoft.AspNetCore.Mvc;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantsController(IRestaurantService RestaurantService)
        {
            _restaurantService = RestaurantService;
        }

        [HttpGet("GetAll")]
        public ActionResult<GetRestaurantByIdDto> GetAll()
        {
            try
            {
                IEnumerable<GetRestaurantByIdDto> restaurants = _restaurantService.GetAll();
                if (restaurants?.Any() != true)
                {
                    return NoContent();
                }
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("GetOneByid/{userId}")]
        public IActionResult GetOneById(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            try
            {
                GetRestaurantByIdDto? restaurant = _restaurantService.GetById(userId);

                if (restaurant is null)
                {
                    return NotFound();
                }

                return Ok(restaurant);
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

        [HttpGet("IsOpen/{id}")]
        public IActionResult IsOpen(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            try
            {
                var result = _restaurantService.RestaurantIsOpen(id);
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
    }
}
