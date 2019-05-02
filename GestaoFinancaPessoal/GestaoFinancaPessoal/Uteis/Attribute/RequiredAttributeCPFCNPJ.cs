using GestaoFinancaPessoal.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis.Attribute
{
    /// <summary>
    ///     Validation attribute to indicate that a property field or parameter is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class RequiredAttributeCPFCNPJ : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        /// <remarks>
        ///     This constructor selects a reasonable default error message for
        ///     <see cref="ValidationAttribute.FormatErrorMessage" />
        /// </remarks>
        public RequiredAttributeCPFCNPJ(): base()
        {
        }

        /// <summary>
        ///     Gets or sets a flag indicating whether the attribute should allow empty strings.
        /// </summary>
        public bool AllowEmptyStrings { get; set; }

        public void AddValidation(ClientModelValidationContext context)
        {

        }

        /// <summary>
        ///     Override of <see cref="ValidationAttribute.IsValid(object)" />
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <returns>
        ///     <c>false</c> if the <paramref name="value" /> is null or an empty string. If
        ///     <see cref="RequiredAttribute.AllowEmptyStrings" />
        ///     then <c>false</c> is returned only if <paramref name="value" /> is null.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as CPFCNPJ;

            if (obj == null)
            {
                return new ValidationResult("Object Invald");
            }

            if(obj.TipoPessoa == TipoPessoa.FISICA)
            {
                if(obj.RG == null || obj.RG.Length < 5)
                {
                    return new ValidationResult("RG Invalido");
                }
            }
            else
            {
                if(obj.NomeContato == null || obj.NomeContato.Length < 3)
                {
                    return new ValidationResult("Nome de Contato Invalido");
                }
            }

            // only check string length if empty strings are not allowed
            return ValidationResult.Success;
        }
    }
}
