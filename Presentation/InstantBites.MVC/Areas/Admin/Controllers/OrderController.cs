using InstantBites.Application.Features.Commands.Orders.AddOrder;
using InstantBites.Application.Features.Commands.Orders.DeleteOrder;
using InstantBites.Application.Features.Commands.Orders.UpdateOrder;
using InstantBites.Application.Features.Queries.Meals.GetAllMeals;
using InstantBites.Application.Features.Queries.OrderCategories.GetAllOrderCategories;
using InstantBites.Application.Features.Queries.Orders.GetAllOrders;
using InstantBites.Application.Features.Queries.Orders.GetOrder;
using InstantBites.Application.Features.Queries.Restaurants.GetAllRestaurants;
using InstantBites.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Diagnostics;
using System.Net;

namespace InstantBites.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        private static string Id;
        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<IActionResult> GetAll(GetAllOrdersQueryRequest request)
        {
            try
            {
                
                var res = await _mediator.Send(request);
                var restRes = await _mediator.Send(new GetAllRestaurantsQueryRequest());
                var OcRes = await _mediator.Send(new GetAllOrderCategoriesQueryRequest());
                var mRes = await _mediator.Send(new GetAllMealsQueryRequest());
                ViewBag.Orders = res.Orders;
                ViewBag.Restaurant = new SelectList(restRes.Restaurants, "Id", "Name");
                ViewBag.OrderCategories = new SelectList(OcRes.OrderCategories, "Id", "Name");
                ViewBag.Meals = new MultiSelectList(mRes.Meals, "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow}:: {ex.Message}");
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
                _logger.LogError($"{DateTime.UtcNow}:: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,CategoryID,Image,Description,MealIds,RestaurantID,Price")] AddOrderCommandRequest request)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.Success)
                    {
                        return RedirectToAction("GetAll", "Order");
                    }
                    else
                    {
                        _logger.LogError($"{DateTime.UtcNow}:: Order  is not added");
                        return BadRequest(ModelState);
                    }
                }
                _logger.LogError($"{DateTime.UtcNow}::Model State is not valid");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                
                Id = id;
                var req = new GetOrderQueryRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    ViewBag.Order = res.Order;
                    return Ok(res);
                }
                _logger.LogError($"{DateTime.UtcNow} :: Order is not found");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind("Name,CategoryID,Image,Description,MealIds,RestaurantID,Price")] AddOrderCommandRequest request)
        {
            try
            {                
                var req = new UpdateOrderCommandRequest()
                { 
                    Id = Id,
                    Name = request.Name,
                    CategoryID=request.CategoryID,
                    RestaurantID=request.RestaurantID,
                    Price=request.Price,
                    Description=request.Description,
                    MealIds=request.MealIds,
                    Image=request.Image
                };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    return RedirectToAction("GetAll", "Order");
                }
                _logger.LogError($"{DateTime.UtcNow} :: Order is not updated");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {                
                var req = new DeleteOrderCommandRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success) return RedirectToAction("GetAll", "Order");
                _logger.LogError($"{DateTime.UtcNow} ::Order is not Deleted");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
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
