using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinacaPessoal.Models
{
    //Required : Torna o valor do campo obrigatório.
    //StringLength: Para definir o comprimento máximo do campo.
    //Range: Para definir o valor mínimo e máximo.
    //DataType: Para definir o tipo suportado pelo campo.
    public class UsuarioModel
    {
        [DisplayName("Primeiro Nome")]
        [StringLength(50, ErrorMessage = "O campo Nome permite no máximo 50 caracteres!")]
        public string nome { get; set; }

        [Required]
        public string sobrenome { get; set; }

        public string endereco { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Informe o Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email inválido.")]
        public string email { get; set; }

        [DataType(DataType.Date)]
        public DateTime nascimento { get; set; }
    }
}
