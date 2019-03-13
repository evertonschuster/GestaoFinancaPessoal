using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinacaPessoal.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public string Id { get; set; }

        [DisplayName("Primeiro Nome")]
        [StringLength(50, ErrorMessage = "O campo Nome permite no máximo 50 caracteres!")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Sobrenome { get; set; }

        [MaxLength(100)]
        public string Endereco { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Informe o Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [MaxLength(100)]
        [Required]
        public string Senha { get; set; }
    }
}
