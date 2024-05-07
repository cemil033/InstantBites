using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.Services;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using InstantBites.Domain.Services;
using MediatR;
using Microsoft.Identity.Client.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Orders.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommandRequest, AddOrderCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhotoManager _photoManager;

        public AddOrderCommandHandler(IUnitOfWork unitOfWork, PhotoManager photoManager)
        {
            _unitOfWork = unitOfWork;
            _photoManager = photoManager;
        }

        public async Task<AddOrderCommandResponse> Handle(AddOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    Price = request.Price,
                    CategoryId = request.CategoryID,
                    RestaurantId = request.RestaurantID,
                    Name = request.Name,
                    Description = request.Description,
                };
                if (request.Image != null)
                {
                    order.SignedUrl = _photoManager.AddPhoto(request.Image);
                }
                var meals = _unitOfWork.R_Meal.GetAll();

                foreach (var mealid in request.MealIds)
                {

                    await _unitOfWork.W_OrderMeals.AddAsync(new() { Id = Guid.NewGuid().ToString(), MealId = mealid, OrderId = order.Id });
                    order.TotalCalories += meals.FirstOrDefault(e => e.Id == mealid).Calories;
                    order.TotalWeight += meals.FirstOrDefault(e => e.Id == mealid).Calories;
                }
                var result =  await _unitOfWork.W_Order.AddAsync(order);
                if (result) await _unitOfWork.W_Order.SaveChangesAsync();

                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
       
    }
}
