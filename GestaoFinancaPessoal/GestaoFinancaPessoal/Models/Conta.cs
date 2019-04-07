using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Models
{
    public class Conta : MasterModel
    {
        [DataMember]
        [StringLength(256, ErrorMessage = "A {0} deve ter no máximo {1} caracteres.")]
        [Display(Name = "Descrição da Conta")]
        public string Descricao { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Nome da Conta.")]
        [StringLength(256, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome da Conta")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o saldo da conta.")]
        [DataMember]
        public double Saldo { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Tipo da Conta.")]
        [StringLength(256, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Tipo da Conta")]
        public string Tipo { get; set; }

        [DataMember]
        [Display(Name = "Banco da Conta")]
        public string Banco { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public Boolean IsSuspensa { get; set; }

    }
}
