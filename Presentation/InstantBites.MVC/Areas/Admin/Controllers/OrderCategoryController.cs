using InstantBites.Application.Features.Commands.MealCategories.AddMealCategory;
using InstantBites.Application.Features.Commands.MealCategories.DeleteMealCategory;
using InstantBites.Application.Features.Commands.MealCategories.UpdateMealCategory;
using InstantBites.Application.Features.Commands.OrderCategories.AddOrderCategory;
using InstantBites.Application.Features.Commands.OrderCategories.DeleteOrderCategory;
using InstantBites.Application.Features.Commands.OrderCategories.UpdateOrderCategory;

using InstantBites.Application.Features.Queries.MealCategories.GetAllMealCategories;
using InstantBites.Application.Features.Queries.MealCategories.GetMealCategory;
using InstantBites.Application.Features.Queries.OrderCategories.GetAllOrderCategories;
using InstantBites.Application.Features.Queries.OrderCategories.GetOrderCategory;
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
    public class OrderCategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderCategoryController> _logger;
        private static string Id;
        public OrderCategoryController(IMediator mediator, ILogger<OrderCategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<IActionResult> GetAll(GetAllOrderCategoriesQueryRequest request)
        {
            try
            {
                
                var res = await _mediator.Send(request);
                ViewBag.Categories = res.OrderCategories;
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
        public async Task<IActionResult> Add([Bind("Name")] AddOrderCategoryCommandRequest request)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.Success)
                    {
                        return RedirectToAction("GetAll", "OrderCategory");
                    }
                    else
                    {
                        _logger.LogError($"{DateTime.UtcNow}:: Order Category is not added");
                        return BadRequest();
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
                var req = new GetOrderCategoryQueryRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    ViewBag.OrderCategory = res.OrderCategory;
                    return Ok(res.OrderCategory.Name);
                }
                _logger.LogError($"{DateTime.UtcNow} :: Order Category is not found");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind("Name")] AddOrderCategoryCommandRequest request)
        {
            try
            {
                
                var req = new UpdateOrderCategoryCommandRequest() { Id = Id, Name = request.Name };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    return RedirectToAction("GetAll", "OrderCategory");
                }
                _logger.LogError($"{DateTime.UtcNow} :: Order Category is not updated");
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
                var req = new DeleteOrderCategoryCommandRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success) return RedirectToAction("GetAll", "OrderCategory");
                _logger.LogError($"{DateTime.UtcNow} ::Order Category is not Deleted");
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
