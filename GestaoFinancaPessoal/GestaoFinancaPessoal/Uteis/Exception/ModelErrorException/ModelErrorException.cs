using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis.Exception.ModelErrorException
{
    public class ModelErrorException : System.Exception
    {
        List<ModelError> modelErrors = new List<ModelError>();

        public ModelErrorException()
        {
        }

        public ModelErrorException(List<ModelError> modelErrors)
        {
            this.modelErrors = modelErrors ?? throw new ArgumentNullException(nameof(modelErrors));
        }

        public ModelErrorException(ModelError modelErrors)
        {
            this.modelErrors.Add(modelErrors ?? throw new ArgumentNullException(nameof(modelErrors)));
        }



        public List<ModelError> GetModelErrors()
        {
            return modelErrors;
        }

      

    }
}
