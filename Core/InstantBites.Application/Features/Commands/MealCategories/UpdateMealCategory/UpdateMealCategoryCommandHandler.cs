using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.MealCategories.UpdateMealCategory
{
    public class UpdateMealCategoryCommandHandler : IRequestHandler<UpdateMealCategoryCommandRequest, UpdateMealCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMealCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateMealCategoryCommandResponse> Handle(UpdateMealCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var mealcategory = await _unitOfWork.R_MealCategory.Get(request.Id, false);
                mealcategory.Name = request.Name;
                var result = await _unitOfWork.W_MealCategory.Update(mealcategory);
                if (result) await _unitOfWork.W_MealCategory.SaveChangesAsync();

                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
