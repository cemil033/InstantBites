using InstantBites.Application.Validation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.RegistrationClient
{
    public class RegistrationClientCommandRequest:IRequest<RegistrationClientCommandResponse>
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [EmailUserUnique]
        [Display(Name = "Email")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.PhoneNumber)]
        [PhoneValid]
        [PhoneUnique]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [PasswordValid]
        [Compare("Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Username is required")]
        [Display(Name="UserName")]
        [UserNameUnique]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        [Range(0, 999999)]
        public double Balance { get; set; }

        [Required(ErrorMessage = "Mark your Location")]
        public double Longitude { get; set; }         
        public double Latitude { get; set; }
        public bool Active { get; set; }
        public IFormFile Image { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }        
    }
}
