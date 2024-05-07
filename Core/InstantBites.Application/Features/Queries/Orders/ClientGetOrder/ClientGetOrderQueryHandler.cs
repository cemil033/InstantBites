using InstantBites.Application.Features.Queries.Orders.GetOrder;
using InstantBites.Application.Repositories.RestaurantLocationRepository;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Orders.ClientGetOrder
{
    public class ClientGetOrderQueryHandler:IRequestHandler<ClientGetOrderQueryRequest,ClientGetOrderQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClientGetOrderQueryHandler(IUnitOfWork unitOfWork, SignInManager<Client> signInManager, UserManager<Client> userManager, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<ClientGetOrderQueryResponse> Handle(ClientGetOrderQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.R_Order.Get(request.Id, false);
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                order.Category = await _unitOfWork.R_OrderCategory.Get(order.CategoryId, false);
                var rest = await _unitOfWork.R_Restaurant.Get(order.RestaurantId, false);
                rest.RestaurantLocation = await _unitOfWork.R_RestaurantLocation.Get(rest.RestaurantLocationId, false);
                order.Restaurant = rest;
                List<string> ids = new();
                List<Meal> getMeals = new();
                double shipping = 0;
                var issign=_signInManager.IsSignedIn(_contextAccessor.HttpContext.User);
                if (issign)
                {
                    var uloc = await _unitOfWork.R_ClientLocation.Get(user.ClientLocationId, false);
                    shipping = Math.Floor((DistanceTo(rest.RestaurantLocation.Latitude, rest.RestaurantLocation.Longitude, uloc.Latitude, uloc.Longitude) * 0.02) * 100)/ 100;
                }                
                var meals = _unitOfWork.R_Meal.GetAll(false).ToList();
                foreach (var item in _unitOfWork.R_OrderMeals.GetAll(false))
                {
                    if (item.OrderId == request.Id)
                    {
                        getMeals.Add(meals.FirstOrDefault(m => m.Id == item.MealId));                        
                    }
                }
                if (order == null) return new() { Success = false };
                return new() { Success = true, Order = order, Meals = getMeals,Shipping=shipping };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 160.9344;
        }
    }
}
