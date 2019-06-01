using GestaoFinancaPessoal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class DashBoardInicial
    {

        public decimal SaldoAtual { get; set; }

        public decimal TotalDespesaMes { get; set; }

        public decimal TotalReceitaaMes { get; set; }

        public IList<ContaViewModel> Conta { get; set; }
    }
}
