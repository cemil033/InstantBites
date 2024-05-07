using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.MealCategories.AddMealCategory
{
    public class AddMealCategoryCommandResponse
    {
        public bool Success { get; set; }
        public string Title { get; set; }
        public Dictionary<string, string> Results { get; set; }
       
    }
}
