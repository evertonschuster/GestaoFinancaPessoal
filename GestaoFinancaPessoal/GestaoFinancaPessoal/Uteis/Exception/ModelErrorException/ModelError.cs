using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis.Exception.ModelErrorException
{
    public class ModelError
    {
        public string Key { get; set; }
        public string ErroMessage { get; set; }

        public ModelError(string key, string erroMessage)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ErroMessage = erroMessage ?? throw new ArgumentNullException(nameof(erroMessage));
        }
    }
}
