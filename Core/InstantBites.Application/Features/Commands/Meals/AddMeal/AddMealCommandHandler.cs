using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.Services;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using InstantBites.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Meals.AddMeal
{
    public class AddMealCommandHandler : IRequestHandler<AddMealCommandRequest, AddMealCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PhotoManager _photoManager;
        
        public AddMealCommandHandler(IUnitOfWork unitOfWork, PhotoManager photoManager)
        {
            _unitOfWork = unitOfWork;
            _photoManager = photoManager;
        }

        public async Task<AddMealCommandResponse> Handle(AddMealCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Meal meal = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Calories = request.Calories,
                    Weight = request.Weight,
                    Name = request.Name,
                    MealCategoryId = request.MealCategoryID,
                };
                if (request.Image != null)
                {
                    meal.SignedUrl = _photoManager.AddPhoto(request.Image);
                }
                var result = await _unitOfWork.W_Meal.AddAsync(meal);
                if (result) await  _unitOfWork.W_Meal.SaveChangesAsync();
                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
                     
        }
        
    }
}
