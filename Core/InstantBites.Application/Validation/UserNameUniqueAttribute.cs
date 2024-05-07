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
using System.Web.Helpers;

namespace InstantBites.Application.Validation
{
    public class UserNameUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
           
            var usermanager = (UserManager<Client>)validationContext.GetService(typeof(UserManager<Client>));
            var entity = usermanager.FindByNameAsync(value.ToString()).Result;
            var usrcl = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var user = usermanager.GetUserAsync(usrcl.HttpContext.User).Result;
            if (user!=null&&user.UserName == value.ToString())
                return ValidationResult.Success;

            if (entity != null)
            {
                return new ValidationResult($"UserName {value.ToString()} is already in use.");
            }
            return ValidationResult.Success;
        }

    }
}
