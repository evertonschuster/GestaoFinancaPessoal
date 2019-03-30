using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Models
{
    public class Categoria : MasterModel
    {
        [DataMember]
        [StringLength(256, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe o nome da Categoria")]
        public string Nome { get; set; }

        [DataMember]
        [Display(Name = "Hierarquia da categoria")]
        public Categoria Hierarquia { get; set; }
    }
}
