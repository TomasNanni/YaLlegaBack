using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YaLlega.Entities;
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
        public ActionResult<GetRestaurantByIdDto> GetAll()
        {
            IEnumerable<GetRestaurantByIdDto> restaurants = _restaurantService.GetAll();
            if (restaurants?.Any() != true)
            {
                return NoContent();
            }
            else
            {
                return Ok(restaurants);
            }
        }

        [HttpGet("GetOneByid")]
        [Authorize]
        public IActionResult GetOneById()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId <= 0)
            {
                return BadRequest("El ID ingresado debe ser mayor a 0");
            }

            GetRestaurantByIdDto? restaurant = _restaurantService.GetById(userId);

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update(NewUpdatedRestaurantDTO updatedRestaurant)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            ServiceResult result = _restaurantService.Update(updatedRestaurant, userId);
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpGet("IsOpen/{id}")]
        public IActionResult IsOpen (int id)
        {
            var result = _restaurantService.RestaurantIsOpen(id) == null;
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
