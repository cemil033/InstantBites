using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Meals.GetAllMeals
{
    public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQueryRequest, GetAllMealsQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMealsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllMealsQueryResponse> Handle(GetAllMealsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var meals = _unitOfWork.R_Meal.GetAll(tracking: false).ToList();

                return new()
                {
                    Meals = meals
                };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
