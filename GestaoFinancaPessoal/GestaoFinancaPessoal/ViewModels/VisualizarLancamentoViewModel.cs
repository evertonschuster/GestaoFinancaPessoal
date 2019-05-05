using GestaoFinancaPessoal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class VisualizarLancamentoViewModel
    {
        public VisualizarLancamentoViewModel()
        {
            this.Conta = new Conta();
            this.TipoLancamento = TipoLancamento.TODOS;
        }

        [DataMember]
        [Display(Name = "Data Inicial")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataInicial { get; set; }

        [DataMember]
        [Display(Name = "Data Final")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataFinal { get; set; }

        [DataMember]
        [Display(Name = "Tipo de Lançamento")]
        public TipoLancamento TipoLancamento { get; set; }

        [DataMember]
        [Display(Name = "Pendentes de Vencidos")]
        public bool PendenteVencidos { get; set; }

        [DataMember]
        [Display(Name = "Proximos a Vencer")]
        public bool ProximoVencer { get; set; }

        [DataMember]
        [Display(Name = "Conta")]
        public Conta Conta { get; set; }

        [DataMember]
        public Notificacao Notificacao { get; set; }

    }
}
