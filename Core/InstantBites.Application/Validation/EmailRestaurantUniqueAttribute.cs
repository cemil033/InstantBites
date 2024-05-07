using InstantBites.Application.Common;
using InstantBites.Application.UnitOfWork;
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
    public class EmailRestaurantUniqueAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(
           object value, ValidationContext validationContext)
        {
            var uow = (IUnitOfWork)validationContext.GetService(typeof(IUnitOfWork));

            if (!string.IsNullOrEmpty(StaticID.Id)) {
                var rest = uow.R_Restaurant.Get(StaticID.Id, false).Result;
                if (rest != null && rest.Email == value.ToString()){
                  return  ValidationResult.Success;
                }
            }


            var entity = uow.R_Restaurant.GetAll().FirstOrDefault(e=>e.Email==value.ToString());

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
