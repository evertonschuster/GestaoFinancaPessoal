using GestaoFinancaPessoal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    [DataContract]
    public class CategoriaViewModel : Categoria
    {
        public CategoriaViewModel(Categoria categoria, Boolean hasLancamento)
        {
            this.Id = categoria.Id;
            this.Nome = categoria.Nome;
            this.Hierarquia = categoria.Hierarquia;
            this.IsSuspenco = categoria.IsSuspenco;
            this.HasLancamento = hasLancamento;

        }

        [DataMember]
        [Column("HasLancamento")]
        public Boolean HasLancamento { get; set; }
    }
}
