using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.MealCategories.AddMealCategory
{
    public class AddMealCategoryCommandHandler : IRequestHandler<AddMealCategoryCommandRequest, AddMealCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddMealCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
        public async Task<AddMealCategoryCommandResponse> Handle(AddMealCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var mealcategory = new MealCategory()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name
                };
                var result = await _unitOfWork.W_MealCategory.AddAsync(mealcategory);
                if (result)
                {
                    await _unitOfWork.W_MealCategory.SaveChangesAsync();
                    return new()
                    {
                        Success = true,
                        Title = "Add MealCategory"
                    };
                }
                var response = new AddMealCategoryCommandResponse()
                {
                    Success = false,
                    Title = "Add MealCategory"
                };
                response.Results.Add("Add", "Unsuccess");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
