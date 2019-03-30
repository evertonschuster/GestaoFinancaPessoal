using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Models
{
    public class Recorrente : MasterModel
    {

        public int Quantidade { get; set; }

        public int Periodo { get; set; }

        public decimal ParcelaInicial { get; set; }

        public decimal ParcelaTotal { get; set; }

        public decimal Valor { get; set; }
    }
}
