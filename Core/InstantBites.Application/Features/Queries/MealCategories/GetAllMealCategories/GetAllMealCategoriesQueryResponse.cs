using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.MealCategories.GetAllMealCategories
{
    public class GetAllMealCategoriesQueryResponse
    {        
        public List<MealCategory> Categoires { get; set; }
    }
}
