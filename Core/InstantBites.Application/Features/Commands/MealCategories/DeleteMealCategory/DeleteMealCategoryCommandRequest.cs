using InstantBites.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.MealCategories.DeleteMealCategory
{
    public class DeleteMealCategoryCommandRequest:IRequest<DeleteMealCategoryCommandResponse>
    {
        public string Id { get; set; }
    }
}
