using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Orders.AddOrder
{
    public class AddOrderCommandRequest:IRequest<AddOrderCommandResponse>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 999999)]
        public double Price { get; set; }

        public double TotalWeight { get; set; }
        public double TotalCalories { get; set; }
        public List<string> MealIds { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string CategoryID { get; set; }

        [Required(ErrorMessage = "Restaurant is required")]
        public string RestaurantID { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
