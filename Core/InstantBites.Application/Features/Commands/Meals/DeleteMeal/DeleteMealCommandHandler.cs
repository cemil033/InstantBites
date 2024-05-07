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
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Meals.DeleteMeal
{
    public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommandRequest, DeleteMealCommandResponse>
    {        
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteMealCommandResponse> Handle(DeleteMealCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.W_Meal.RemoveAsync(request.Id);
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
