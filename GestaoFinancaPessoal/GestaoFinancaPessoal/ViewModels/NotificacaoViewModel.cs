using GestaoFinancaPessoal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class NotificacaoViewModel : MasterModel
    {
        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public DateTime DataVence { get; set; }

        [DataMember]
        public TipoLancamento TipoLancamento { get; set; }


        [DataMember]
        public string STRTipoLancamento
        {
            get
            {
                return this.TipoLancamento.ToString();
            }
        }
    }
}
