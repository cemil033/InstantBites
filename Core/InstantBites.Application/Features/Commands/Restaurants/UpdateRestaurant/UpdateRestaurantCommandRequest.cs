using InstantBites.Application.Validation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Restaurants.UpdateRestaurant
{
    public class UpdateRestaurantCommandRequest:IRequest<UpdateRestaurantCommandResponse>   
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public string? SavedUrl { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public string Id { get; set; }
        public IFormFile? Image { get; set; }
    }
}
