using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis.Exception.RegistrerException
{
    public class ErroInternalRegisterException : System.Exception
    {
        public ErroInternalRegisterException()
        {
        }

        public ErroInternalRegisterException(string message) : base(message)
        {
        }

        public ErroInternalRegisterException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ErroInternalRegisterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static String PathSavaException { get; private set; }
        public static String Object { get; private set; }



    }
}
