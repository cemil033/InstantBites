using InstantBites.Application.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Clients.LoginClient
{
    public class LoginClientCommandRequest:IRequest<LoginClientCommandResponse>
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]        
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [PasswordValid]
        [Compare("Password")]
        public string Password { get; set; }
    }
}
