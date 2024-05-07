using InstantBites.Application.Features.Commands.MealCategories.AddMealCategory;
using InstantBites.Application.Features.Commands.MealCategories.DeleteMealCategory;
using InstantBites.Application.Features.Commands.MealCategories.UpdateMealCategory;

using InstantBites.Application.Features.Queries.MealCategories.GetAllMealCategories;
using InstantBites.Application.Features.Queries.MealCategories.GetMealCategory;
using InstantBites.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InstantBites.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MealCategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MealCategoryController> _logger;
        private static string Id; 
        public MealCategoryController(IMediator mediator, ILogger<MealCategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<IActionResult> GetAll(GetAllMealCategoriesQueryRequest request)
        {
            try
            {
                
                var res = await _mediator.Send(request);                
                ViewBag.Categories = res.Categoires;
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
        public async Task<IActionResult> Add([Bind("Name")] AddMealCategoryCommandRequest request)
        {
            try
            {               
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.Success)
                    {
                        return RedirectToAction("GetAll", "MealCategory");
                    }
                    else
                    {
                        _logger.LogError($"{DateTime.UtcNow}:: Meal Category  is not added");
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
                var req = new GetMealCategoryQueryRequest() { Id = id };
                var res = await _mediator.Send(req);
                if (res.Success)
                {
                    ViewBag.MealCategory = res.mealCategory;
                    return Ok(res.mealCategory.Name);
                }
                _logger.LogError($"{DateTime.UtcNow} :: Meal Category is not found");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.UtcNow} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind("Name")] AddMealCategoryCommandRequest request)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var req = new UpdateMealCategoryCommandRequest() { Id = Id, Name = request.Name };
                    var res = await _mediator.Send(req);
                    if (res.Success)
                    {
                        return RedirectToAction("GetAll", "MealCategory");
                    }
                    _logger.LogError($"{DateTime.UtcNow} :: Meal Category is not updated");
                    return BadRequest();
                }
                else
                {
                    _logger.LogError($"{DateTime.UtcNow} :: Model State is not valid");
                    return BadRequest(ModelState);
                }
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
                var req = new DeleteMealCategoryCommandRequest() { Id = id };
                var res = await _mediator.Send(req);
                if(res.Success) return RedirectToAction("GetAll","MealCategory");
                _logger.LogError($"{DateTime.UtcNow} ::Meal Category is not Deleted");
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
