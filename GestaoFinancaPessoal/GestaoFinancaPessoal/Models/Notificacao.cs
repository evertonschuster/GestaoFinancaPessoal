using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Models
{
    public class Notificacao : MasterModel
    {
        [DataMember]
        [Required(ErrorMessage = "Informe o Tempo.")]
        public int Tempo { get; set; }

        [DataMember]
        public TipoPeriodicidadeNotificacao Periodicidade { get; set; }

        public DateTime DataInicio
        {
            get
            {
                var data = DateTime.Now;
                data.AddHours(this.Tempo * (int)this.Periodicidade );
                return data;
            }
        }
    }

}
