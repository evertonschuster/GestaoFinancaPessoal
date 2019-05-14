using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class CalendarEvent
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public string TextColor { get; set; }

        [DataMember]
        public string ClassName { get; set; }


        [DataMember]
        public bool AllDay { get; set; }


        [DataMember]
        public bool description { get; set; }

        [DataMember]
        public decimal Valor { get; set; }

        [DataMember]
        public decimal ValorPago { get; set; }


        [DataMember]
        public string DsConta{ get; set; }
    }
}
