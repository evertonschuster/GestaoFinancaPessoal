using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Models
{
    public class Lancamento : MasterModel
    {
        [DataMember]
        [Display(Name = "Conta")]
        [Required(ErrorMessage = "Informe a conta.")]
        public Conta Conta { get; set; }

        [DataMember]
        //[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Informe o valor.")]
        public decimal Valor { get; set; }

        [DataMember]
        //[Range(0, 100, ErrorMessage = "Some another error message insert here!")]
        //[RegularExpression(@"^(?:[1-9](?:[\d]{0,2}(?:\.[\d]{3})*|[\d]+)|0)(?:[,.][\d]{0,2})?$", ErrorMessage = "Valor deve ser númerico.")]
        //[DisplayFormat(DataFormatString = "{0:[###]}", ApplyFormatInEditMode = true)]
        [Display(Name = "Valor pago")]
        [Required(ErrorMessage = "Informe o valor pago.")]
        public decimal ValorPago { get; set; }

        [DataMember]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição.")]
        [StringLength(256, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }

        //[DataMember]
        //[Display(Name = "Pago?")]
        //public bool IsPago { get; set; }

        [DataMember]
        [Display(Name = "Lançamento automatico?")]
        public bool IsAutomatico { get; set; }

        [DataMember]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data de pagamento")]
        [Required(ErrorMessage = "Informe a data de pagamento.")]
        public DateTime DataPagamento { get; set; }

        [DataMember]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data de vencimento")]
        [Required(ErrorMessage = "Informe a data de vencimento.")]
        public DateTime DataVencimento { get; set; }

        [DataMember]
        [Display(Name = "Subcategoria")]
        [Required(ErrorMessage = "Informe a subcategoria.")]
        public Categoria Categoria { get; set; }

        [DataMember]
        [Display(Name = "Tipo lançamento")]
        [Required(ErrorMessage = "Informe o tipo do lançamento.")]
        public TipoLancamento Tipo { get; set; }

        [DataMember]
        [Display(Name = "Recorrente")]
        public Recorrente Recorrente { get; set; }

        public DateTime DataInclusao { get; set; }

        [DataMember]
        [Display(Name = "Conta destino")]
        public Conta ContaDestion { get; set; }

    }

    public enum TipoLancamento
    {
        [Display(Name = "Despesa")]
        DESPESA,
        [Display(Name = "Receita")]
        RECEITA,
        [Display(Name = "Transferencia")]
        TRANSFERENCIA
    }
}
