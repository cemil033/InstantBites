using InstantBites.Application.Features.Commands.Orders.StopSubscribeOrder;
using InstantBites.Application.Features.Commands.Orders.SubscribeOrder;
using InstantBites.Application.Features.Queries.Meals.GetAllMeals;
using InstantBites.Application.Features.Queries.Meals.GetMeal;
using InstantBites.Application.Features.Queries.OrderCategories.GetAllOrderCategories;
using InstantBites.Application.Features.Queries.Orders.ClientGetOrder;
using InstantBites.Application.Features.Queries.Orders.GetAllOrders;
using InstantBites.Application.Features.Queries.Orders.GetCategoryOrders;
using InstantBites.Application.Features.Queries.Orders.GetOrder;
using InstantBites.Application.Features.Queries.Orders.GetRestaurantOrders;
using InstantBites.Application.Features.Queries.Restaurants.GetAllRestaurants;
using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Services;
using InstantBites.Domain.Services;
using InstantBites.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using static System.Net.WebRequestMethods;

namespace InstantBites.MVC.Controllers
{
    [Authorize(Roles = "Client")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
       
        public HomeController(ILogger<HomeController> logger, IMediator mediator, IEmailService emailService)
        {
            _logger = logger;
            _mediator = mediator;
            _emailService = emailService;
            
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {                
                var orders = await _mediator.Send(new GetAllOrdersQueryRequest());
                ViewBag.Restaurants=(await _mediator.Send(new GetAllRestaurantsQueryRequest())).Restaurants;
                ViewBag.Ordercategory = (await _mediator.Send(new GetAllOrderCategoriesQueryRequest())).OrderCategories;
                return View(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }            
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetOrdersByRestaurant(string id)
        {
            try
            {
                var res = await _mediator.Send(new GetRestaurantOrdersQueryRequest() { Id = id });
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetOrdersByCategory(string id)
        {
            try
            {
                var res=await _mediator.Send(new GetCategoryOrdersQueryRequest() { Id = id });
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Restaurants()
        {
            try
            {
                var res = await _mediator.Send(new GetAllRestaurantsQueryRequest());
                return View(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {               
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);                
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                
                var res = await _mediator.Send(new ClientGetOrderQueryRequest() { Id = id });
                if (res.Success) { return Ok(res); }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetMeal(string id)
        {
            try
            {
                var res=await _mediator.Send(new GetMealQueryRequest() { Id= id });
                if(res.Success) { return Ok(res); }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<IActionResult> Subscribe(string orderId)
        {
            try
            {                
                var res = await _mediator.Send(new SubscribeOrderCommandRequest() { Id = orderId });
                if( res.Success) {
                    double longi = res.Restaurant.RestaurantLocation.Longitude;
                    double lati = res.Restaurant.RestaurantLocation.Latitude;
                    await _emailService.SendMessage(res.Restaurant.Email, $"You have a new subscription to the {res.Order.Name} menu.\n Subscription Time : {DateTime.Now} \n Subscription until : {DateTime.Now.AddDays(30)} \n Address:: https://maps.google.com/?q={lati},{longi}", Url);
                    
                    return Ok(new { success = res.Success,message="Subscription is Success"}); 
                }
                else if(res.IsSubscribed) { return Ok(new { success = res.Success, message = "You alredy have this subscription" }); }
                else{ return Ok(new  {success= res.Success,message= "You have not enugh balance for subscription" }); }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }            
        }
        [AllowAnonymous]
        public async Task<IActionResult> Result(bool success,string message)
        {
            try
            {
                ViewBag.Success = success;
                ViewBag.Message = message;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<IActionResult> StopSubscribe(string id)
        {
            try
            {
                var res=await _mediator.Send(new StopSubscribeOrderCommandRequest() { OrderId= id });
                return RedirectToAction("Result", "Home", new { success = res.Success, message = "Your subscription is canceled" }); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> About()
        {
            try
            {                
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
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