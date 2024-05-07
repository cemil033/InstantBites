using InstantBites.Application.Validation;
using InstantBites.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Restaurants.AddRestaurant
{
    public class AddRestaurantCommandRequest:IRequest<AddRestaurantCommandResponse>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mark your Location")]
        public double Longitude { get; set; }
        [Required(ErrorMessage = "Mark your Location")]
        public double Latitude { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [EmailRestaurantUnique]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public IFormFile? Image { get; set; }   
    }
}
