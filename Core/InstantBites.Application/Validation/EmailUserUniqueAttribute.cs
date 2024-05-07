using Azure.Core;
using InstantBites.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Validation
{
    public class EmailUserUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var usermanager = (UserManager<Client>)validationContext.GetService(typeof(UserManager<Client>));
            var usrcl = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var user = usermanager.GetUserAsync(usrcl.HttpContext.User).Result;
            if (user != null && user.Email == value.ToString())
                return ValidationResult.Success;

            var entity = usermanager.FindByEmailAsync(value.ToString()).Result;

            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string email)
        {
            return $"Email {email} is already in use.";
        }
    }
}
