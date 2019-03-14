using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis.Attribute
{
    public class ParameterSystemAttribute : System.Attribute
    {
        public string DefaultValue { get; set; }
        public string NameParameter { get; set; }
        public string DescriptionParameter { get; set; }
    }
}
