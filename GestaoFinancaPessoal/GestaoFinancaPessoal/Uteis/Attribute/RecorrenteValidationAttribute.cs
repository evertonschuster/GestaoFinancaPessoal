using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis.Attribute
{
    public class RecorrenteValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var movie = (Movie)validationContext.ObjectInstance;
            //var releaseYear = ((DateTime)value).Year;

            //if (movie.Genre == Genre.Classic && releaseYear > _year)
            //{
            //    return new ValidationResult(GetErrorMessage());
            //}

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Classic movies must have a release year no later than.";
        }
    }
}
