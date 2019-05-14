using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class ReceitaDespesa
    {
        [DataMember]
        public decimal ValorDespesa { get; set; }

        [DataMember]
        public String DescricaoDespensa { get; set; }

        [DataMember]
        public decimal ValorReceita { get; set; }

        [DataMember]
        public String DescricaoReceita { get; set; }

        [DataMember]
        public string DataLancamento { get; set; }

        [DataMember]
        public String AnoMesLancamento { get; set; }

    }
}
