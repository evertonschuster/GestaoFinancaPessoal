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

        [DataMember]
        [Display(Name = "Pago?")]
        public bool IsPago { get; set; }

        [DataMember]
        [Display(Name = "Lançamento automatico?")]
        public bool IsAutomatico { get; set; }

        [DataMember]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data de pagamento")]
        //[Required(ErrorMessage = "Informe a data de pagamento.")]
        public DateTime? DataPagamento { get; set; } = DateTime.Now;

        [DataMember]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data de vencimento")]
        [Required(ErrorMessage = "Informe a data de vencimento.")]
        public DateTime DataVencimento { get; set; } = DateTime.Now;

        [DataMember]
        [Display(Name = "Subcategoria")]
        [Required(ErrorMessage = "Informe a subcategoria.")]
        public Categoria Categoria { get; set; }

        [DataMember]
        [Display(Name = "Tipo lançamento")]
        [Required(ErrorMessage = "Informe o tipo do lançamento.")]
        public TipoLancamento TipoLancamento { get; set; }

        [DataMember]
        [Display(Name = "Recorrente")]
        public Recorrente Recorrente { get; set; }

        [DataMember] 
        public DateTime DataInclusao { get; set; }

        [DataMember]
        [Display(Name = "Conta destino")]
        public Conta ContaDestion { get; set; }

        [DataMember]
        [Column("Notificacao")]
        public DateTime DataNotificacao { get; set; }


        [DataMember]
        [Required(ErrorMessage = "Informe o Tempo.")]
        public int Tempo { get; set; }

        [DataMember]
        [Column("Periodicidade")]
        public TipoPeriodicidadeNotificacao PeriodicidadeNotificacao { get; set; }

        public TimeSpan DataInicio
        {
            get
            {
                var minutos = this.Tempo * (int)this.PeriodicidadeNotificacao;
                return new TimeSpan(0,minutos,0);
            }
        }
    }

    public enum TipoLancamento
    {
        [Display(Name = "Despesa")]
        DESPESA,
        [Display(Name = "Receita")]
        RECEITA,
        [Display(Name = "Transferencia")]
        TRANSFERENCIA,
        [Display(Name = "TODOS")]
        TODOS
    }

    public enum TipoPeriodicidadeNotificacao
    {
        MINUTOS = 1,
        HORAS = 60,
        DIAS = 1440,
        SEMANAS = 10080
    }
}
