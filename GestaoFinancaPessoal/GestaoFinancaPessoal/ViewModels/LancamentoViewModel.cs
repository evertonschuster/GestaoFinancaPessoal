using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class LancamentoViewModel : Lancamento
    {
        [DataMember]
        [RecorrenteValidationAttribute()]
        public RecorrenteViewModel RecorrenteViewModel { get; set; }
    }
}
