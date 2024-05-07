using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Validation
{
    public class PasswordValidAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            var password = value.ToString();
            const int MIN_LENGTH = 8;

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH ;

            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }
            else
            {
                return new ValidationResult("Password should not be lesser than 8.");
            }
            if(!hasUpperCaseLetter)
                return new ValidationResult("Password should contain at least one upper case letter.");
            if(!hasLowerCaseLetter)
                return new ValidationResult("Password should contain at least one lower case letter.");
            if(!hasDecimalDigit)
                return new ValidationResult("Password should contain at least one numeric value.");            
            
            return ValidationResult.Success;            

        }
        
    }
}
