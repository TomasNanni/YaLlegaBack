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

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update(NewUpdatedRestaurantDTO updatedRestaurant)
        {
            if (updatedRestaurant == null)
            {
                return BadRequest("Debe proporcionar datos válidos del restaurante.");
            }

            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId <= 0)
                {
                    return Unauthorized("Usuario no validado.");
                }
                ServiceResult result = _restaurantService.Update(updatedRestaurant, userId);
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
