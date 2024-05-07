using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.MealCategories.GetMealCategory
{
    public class GetMealCategoryQueryResponse
    {
        public bool Success { get; set; }
        public MealCategory mealCategory { get; set; }
    }
}
