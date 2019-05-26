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
        [Required(ErrorMessage = "Por Favor selecione a SubCategoria.")]
        public override int Id { get; set; }

        [DataMember]
        [StringLength(256, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe o nome da Categoria")]
        public string Nome { get; set; }

        [DataMember]
        [Display(Name = "Categoria")]
        public Categoria Hierarquia { get; set; }

        [DataMember]
        public Boolean IsSuspenco { get; set; }
    }
}
