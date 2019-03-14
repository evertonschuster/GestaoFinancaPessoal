using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis
{
    public class Session : IDisposable
    {

        private ISession session { get; }

        public Session(IHttpContextAccessor contextAcessor)
        {
            this.session = contextAcessor.HttpContext.Session;

        }

        //private int? GetPedidoId()
        //{
        //    return contextAcessor.HttpContext.Session.GetInt32("pedidoId");
        //}

        public object this[string chave]    // Indexer declaration  
        {
            get
            {
                var valor = session.Get(chave);
                if (valor == null) return null;
                return ByteArrayToObject(valor);
            }
            set
            {
                var valor = ObjectToByteArray(value);
                session.Set(chave, valor);
            }
        }

        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }

        public void  Dispose()
        {
        }
    }
}
