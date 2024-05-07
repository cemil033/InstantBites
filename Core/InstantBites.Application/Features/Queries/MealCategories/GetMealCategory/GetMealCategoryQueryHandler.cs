using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.MealCategories.GetMealCategory
{
    public class GetMealCategoryQueryHandler : IRequestHandler<GetMealCategoryQueryRequest, GetMealCategoryQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMealCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetMealCategoryQueryResponse> Handle(GetMealCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.R_MealCategory.Get(request.Id, tracking: false);
                if (category == null) return new() { Success = false };
                return new()
                {
                    Success = true,
                    mealCategory = category
                };
            }
            catch (Exception)
            {
                throw;
            }
           
        }

    }
}
