using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GestaoFinancaPessoal.Models
{
    [DataContract]
    public class MasterModel :  IClientModelValidator, IDisposable
    {
        [DataMember]
        public int Id { get;  set; }

        public void AddValidation(ClientModelValidationContext context)
        {
            
        }

        public void Dispose()
        {

        }

    }
}
