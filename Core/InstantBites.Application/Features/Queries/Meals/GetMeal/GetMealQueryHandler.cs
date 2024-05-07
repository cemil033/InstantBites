using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Meals.GetMeal
{
    public class GetMealQueryHandler : IRequestHandler<GetMealQueryRequest, GetMealQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMealQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        public async Task<GetMealQueryResponse> Handle(GetMealQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var meal = await _unitOfWork.R_Meal.Get(request.Id, false);
                if (meal == null) return new() { Success = false };
                var mc = await _unitOfWork.R_MealCategory.Get(meal.MealCategoryId, false);
                meal.MealCategory = mc;
                return new() { Success = true, Meal = meal };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
