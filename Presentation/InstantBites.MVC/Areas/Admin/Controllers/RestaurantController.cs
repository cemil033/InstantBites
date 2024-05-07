using InstantBites.Application.Common;
using InstantBites.Application.Features.Commands.Restaurants.AddRestaurant;
using InstantBites.Application.Features.Commands.Restaurants.DeleteRestaurant;
using InstantBites.Application.Features.Commands.Restaurants.UpdateRestaurant;
using InstantBites.Application.Features.Queries.Restaurants.GetAllRestaurants;
using InstantBites.Application.Features.Queries.Restaurants.GetRestaurant;
using InstantBites.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace InstantBites.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RestaurantController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RestaurantController> _logger;
        private static string Id;
        public RestaurantController(IMediator mediator, ILogger<RestaurantController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<IActionResult> GetAll(GetAllRestaurantsQueryRequest request)
        {
            try
            {
                
                var res = await _mediator.Send(request);
                ViewBag.Restaurants = res.Restaurants;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.UtcNow}:: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        public async Task<IActionResult> Add()
        {
            try
            {                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.UtcNow}:: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,Email,Description,Image,Longitude,Latitude")] AddRestaurantCommandRequest request)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.Success)
                    {
                        return Redirect("GetAll");
                    }
                    else
                    {
                        _logger.LogInformation($"{DateTime.UtcNow}:: Restaurant is not added");
                        return BadRequest(ModelState);
                    }
                }
                _logger.LogInformation($"{DateTime.UtcNow}::Model State is not valid");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                
                Id = id;
                
                var req = new GetRestaurantQueryRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    ViewBag.Restaurant = res.Restaurant;
                    return Ok(res.Restaurant);
                }
                _logger.LogInformation($"{DateTime.UtcNow} :: Restaurant is not found");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind("Name,Image,Email,SignedUrl,Description,Longitude,Latitude")] AddRestaurantCommandRequest request)
        {
            try
            {
                StaticID.Id = Id;
                var req = new UpdateRestaurantCommandRequest() { Id = Id, Name = request.Name,Image=request.Image,Email=request.Email,Description=request.Description,Longitude=request.Longitude,Latitude=request.Latitude };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    StaticID.Id = null;
                    return RedirectToAction("GetAll", "Restaurant");
                }
                _logger.LogInformation($"{DateTime.UtcNow} :: Restaurant is not updated");
                return RedirectToAction("GetAll", "Restaurant");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.UtcNow} :: {ex.Message}");
                return RedirectToAction("GetAll", "Restaurant"); ;
            }

        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                
                var req = new DeleteRestaurantCommandRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success) return RedirectToAction("GetAll", "Restaurant");
                _logger.LogInformation($"{DateTime.UtcNow} ::Restaurant is not Deleted");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
