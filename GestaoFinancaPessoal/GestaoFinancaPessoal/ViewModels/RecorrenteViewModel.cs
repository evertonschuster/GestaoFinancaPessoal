using GestaoFinancaPessoal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class RecorrenteViewModel : Recorrente
    {
        [DataMember]
        [Display(Name = "Repetir a cada")]
        [Required(ErrorMessage = "Informe o periodo de intervalo das repetição.")]
        public int Repetir { get; set; }

        [DataMember]
        [Display(Name = "Periodicidade")]
        public TipoPeriodicidade Periodicidade { get; set; }

        [DataMember]
        [Display(Name = "Avançado")]
        public bool? IsAvancado { get; set; }

        [Display(Name = "Data Final")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Valor Total")]
        public decimal? ValorTotal { get; set; }

        public Recorrente GetRecorrente()
        {

            var recorrente = new Recorrente();

            recorrente.Periodo = (int)this.Periodicidade * (int)this.Repetir;
            recorrente.Quantidade= this.Quantidade - (this.ParcelaInicial == 0 ? this.ParcelaInicial : this.ParcelaInicial - 1);
            recorrente.ParcelaInicial = this.ParcelaInicial;
            recorrente.DataInicial = this.DataInicial;
            recorrente.IsMensal = this.IsMensal;

            return recorrente;
        }
    }


    public enum TipoPeriodicidade
    {
        DIA = 1,
        SEMANA = 7,
        QUINZENA = 15,
        MES = 30,
        SEMESTRE = 180,
        ANO = 365
    }
}
