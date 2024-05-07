using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace InstantBites.Application.Validation
{
    public class PhoneValidAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {   
            var reg = new Regex("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$");
            var m=reg.Match(value.ToString());
            if (!m.Success)
                return new ValidationResult($"Phone Number is not correct");        
            return ValidationResult.Success;
        }
    }
}
