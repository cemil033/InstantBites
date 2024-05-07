using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.MealCategories.GetAllMealCategories
{
    public class GetAllMealCategoriesQueryHandler : IRequestHandler<GetAllMealCategoriesQueryRequest, GetAllMealCategoriesQueryResponse>
    {
       private readonly IUnitOfWork _unitOfWork;

        public GetAllMealCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       

        public async Task<GetAllMealCategoriesQueryResponse> Handle(GetAllMealCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categoires = _unitOfWork.R_MealCategory.GetAll(tracking: false).ToList();
                return new()
                {
                    Categoires = categoires
                };
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        
    }
}
