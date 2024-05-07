using InstantBites.Application.Features.Commands.Meals.AddMeal;
using InstantBites.Application.Features.Commands.Meals.DeleteMeal;
using InstantBites.Application.Features.Commands.Meals.UpdateMeal;
using InstantBites.Application.Features.Queries.MealCategories.GetAllMealCategories;
using InstantBites.Application.Features.Queries.Meals.GetAllMeals;
using InstantBites.Application.Features.Queries.Meals.GetMeal;
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
    public class MealController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MealController> _logger;
        private static string Id;
        public MealController(IMediator mediator, ILogger<MealController> logger)
        {

            _mediator = mediator;
            _logger = logger;
        }
        public async Task<IActionResult> GetAll(GetAllMealsQueryRequest request)
        {
            try
            {
                
                var res = await _mediator.Send(request);                
                ViewBag.Meals = res.Meals;
                var mcres = await _mediator.Send(new GetAllMealCategoriesQueryRequest());
                ViewBag.MealCategoryId = new SelectList(mcres.Categoires,"Id", "Name");
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
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,Image,Calories,Weight,MealCategoryID")] AddMealCommandRequest request)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.Success)
                    {
                        return RedirectToAction("GetAll", "Meal");
                    }
                    else
                    {
                        _logger.LogError($"{DateTime.UtcNow}:: Meal is not added");
                        return BadRequest();
                    }
                }
                _logger.LogError($"{DateTime.UtcNow}::Model State Meal is not valid");
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
                var req = new GetMealQueryRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    ViewBag.Meal = res.Meal;
                    return Ok(res);
                }
                _logger.LogError($"{DateTime.UtcNow} :: Meal is not found");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind("Name,Image,Calories,Weight,MealCategoryID")] AddMealCommandRequest request)
        {
            try
            {                
                var req = new UpdateMealCommandRequest() 
                { 
                    Id = Id,
                    Name = request.Name,
                    Calories=request.Calories,
                    Image=request.Image,
                    MealCategoryID=request.MealCategoryID,
                    Weight=request.Weight 
                };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    return RedirectToAction("GetAll", "Meal"); ;
                }
                _logger.LogError($"{DateTime.UtcNow} :: Meal is not updated");
                return BadRequest(ModelState);
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
                
                var req = new DeleteMealCommandRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success) return RedirectToAction("GetAll", "Meal"); ;
                _logger.LogError($"{DateTime.UtcNow} ::Meal is not Deleted");
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
