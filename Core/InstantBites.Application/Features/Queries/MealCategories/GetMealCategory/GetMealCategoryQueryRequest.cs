using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.MealCategories.GetMealCategory
{
    public class GetMealCategoryQueryRequest : IRequest<GetMealCategoryQueryResponse>
    {
        public string Id { get; set; }
    }
}
