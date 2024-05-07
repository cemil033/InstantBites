using InstantBites.Application.Validation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.UpdateClient
{
    public class UpdateClientCommandRequest:IRequest<UpdateClientCommandResponse>
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailUserUnique]
        [PhoneValid]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        
        [DataType(DataType.Password)]        
        [PasswordValid]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]        
        [PasswordValid]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        [Range(0, 999999)]
        public double Balance { get; set; }
        [Required(ErrorMessage = "Adress is required")]        
        public bool Active { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public IFormFile? Image { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
    }
}
