using InstantBites.Application.Repositories.MealRepository;
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

namespace InstantBites.Application.Features.Commands.Meals.UpdateMeal
{
    public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommandRequest, UpdateMealCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhotoManager _photoManager;

        public UpdateMealCommandHandler(IUnitOfWork unitOfWork, PhotoManager photoManager)
        {
            _unitOfWork = unitOfWork;
            _photoManager = photoManager;
        }
       

        public async Task<UpdateMealCommandResponse> Handle(UpdateMealCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var meal = await _unitOfWork.R_Meal.Get(request.Id);
                meal.Weight = request.Weight;
                meal.Calories = request.Calories;
                meal.Name = request.Name;
                meal.MealCategoryId = request.MealCategoryID;
                if (request.Image != null)
                {
                    meal.SignedUrl = _photoManager.AddPhoto(request.Image);
                }
                var result = await _unitOfWork.W_Meal.Update(meal);
                if (result) await _unitOfWork.W_Meal.SaveChangesAsync();
                return new() { Success = result };
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
    }
}
