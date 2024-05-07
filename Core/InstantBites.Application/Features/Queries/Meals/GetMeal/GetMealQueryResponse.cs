using InstantBites.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Queries.Meals.GetMeal
{
    public class GetMealQueryResponse
    {
        public bool Success { get; set; }
        public Meal Meal { get; set; }        
    }
}
