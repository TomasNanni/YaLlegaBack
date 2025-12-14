using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YaLlega.Models;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;
using YaLlegaBack.Services;

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
        [Authorize]
        public ActionResult<RestaurantDataDto> GetAll()
        {
            IEnumerable<RestaurantDataDto> restaurants = _restaurantService.GetAll();
            if (restaurants?.Any() != true)
            {
                return NoContent();
            }
            else
            {
                return Ok(restaurants);
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

            GetRestaurantByIdDto? restaurant = _restaurantService.GetById(id);

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update(NewUpdatedRestaurantDTO updatedRestaurant, int restaurantId)
        {
            ServiceResult result = _restaurantService.Update(updatedRestaurant, restaurantId);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
