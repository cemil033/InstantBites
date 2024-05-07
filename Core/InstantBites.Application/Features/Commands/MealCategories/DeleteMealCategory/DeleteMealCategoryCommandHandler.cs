using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.MealCategories.DeleteMealCategory
{
    public class DeleteMealCategoryCommandHandler : IRequestHandler<DeleteMealCategoryCommandRequest, DeleteMealCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteMealCategoryCommandResponse> Handle(DeleteMealCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var result =await  _unitOfWork.W_MealCategory.RemoveAsync(request.Id);
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
