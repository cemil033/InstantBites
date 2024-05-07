using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Meals.UpdateMeal
{
    public class UpdateMealCommandRequest:IRequest<UpdateMealCommandResponse>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Calories are required")]
        [Range(0, 999999)]
        public double Calories { get; set; }

        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }

        

        [Required(ErrorMessage = "Meal Category ID is required")]
        public string MealCategoryID { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0, 999999)]
        public double Weight { get; set; }
        public IFormFile? Image { get; set; }
        public string Id { get; set; }
    }
}
