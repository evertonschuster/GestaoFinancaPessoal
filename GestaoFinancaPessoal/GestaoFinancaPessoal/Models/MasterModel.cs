using System;
using System.Runtime.Serialization;

namespace GestaoFinancaPessoal.Models
{
    [DataContract]
    public class MasterModel : IDisposable
    {
        [DataMember]
        public int Id { get; protected set; }

        public void Dispose()
        {

        }
    }
}
