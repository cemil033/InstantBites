using InstantBites.Application.Features.Commands.Orders.AddOrder;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Application.Services;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using InstantBites.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Orders.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest, UpdateOrderCommandResponse>
    {
        private readonly PhotoManager _photoManager;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(PhotoManager photoManager, IUnitOfWork unitOfWork)
        {
            _photoManager = photoManager;
            _unitOfWork = unitOfWork;
        }        

        public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.R_Order.Get(request.Id, false);
                order.Price = request.Price;
                order.CategoryId = request.CategoryID;
                order.RestaurantId = request.RestaurantID;
                order.Name = request.Name;
                order.Description = request.Description;
                if (request.Image != null)
                {
                    order.SignedUrl = _photoManager.AddPhoto(request.Image);
                }
                var meals = _unitOfWork.R_Meal.GetAll();
                var mealsRead = _unitOfWork.R_OrderMeals.GetAll();
                foreach (var meal in mealsRead)
                {
                    if (meal.OrderId == order.Id)
                    {
                        _unitOfWork.W_OrderMeals.Remove(meal);
                    }
                }
                order.TotalWeight = 0;
                order.TotalCalories = 0;
                foreach (var mealid in request.MealIds)
                {
                    await _unitOfWork.W_OrderMeals.AddAsync(new() { Id = Guid.NewGuid().ToString(), MealId = mealid, OrderId = order.Id });
                    order.TotalCalories += meals.FirstOrDefault(e => e.Id == mealid).Calories;
                    order.TotalWeight += meals.FirstOrDefault(e => e.Id == mealid).Calories;
                }
                var result = await _unitOfWork.W_Order.Update(order);
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
