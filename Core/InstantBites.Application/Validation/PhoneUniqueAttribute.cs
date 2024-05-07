using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InstantBites.Application.Validation
{
    public class PhoneUniqueAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var uow = (IUnitOfWork)validationContext.GetService(typeof(IUnitOfWork));
            var usermanager = (UserManager<Client>)validationContext.GetService(typeof(UserManager<Client>));
            var entity = uow.R_Client.GetWhere(e => e.PhoneNumber == value.ToString()).ToList();
            var usrcl = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var user = usermanager.GetUserAsync(usrcl.HttpContext.User).Result;
            if (user!=null&&user.PhoneNumber == value.ToString())
                return ValidationResult.Success;
            if (entity.Count > 0)
            {
                return new ValidationResult($"Phone Number is already in use");
            }
            return ValidationResult.Success;
        }
    }
}
