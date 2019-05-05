using GestaoFinancaPessoal.Uteis.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Models
{
    public class Recorrente : MasterModel
    {


        [DataMember]
        [Display(Name = "Repetir a Cada")]
        [Required(ErrorMessage = "Informe o Intervalo de cada repetição.")]
        public int Periodo { get; set; }

        [DataMember]
        [Required (ErrorMessage = "Informe a parcela Inicial.")]
        [Display(Name = "Iniciar na Parcela")]
        public int ParcelaInicial { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe a Quantidade de repetições.")]
        [Display(Name = "Quantidade de Ocorrência")]
        public int Quantidade { get; set; }

        [DataMember]
        [Display(Name = "Mensal")]
        public bool IsMensal { get; set; }

        [DataMember]
        [Display(Name = "Data Inicial")]
        [Required(ErrorMessage = "Informe a data de Inicio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataInicial { get; set; }
    }
}
