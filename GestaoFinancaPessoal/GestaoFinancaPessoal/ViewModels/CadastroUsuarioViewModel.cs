using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        public string Id { get; set; }

        [DisplayName("Primeiro Nome")]
        [StringLength(50, ErrorMessage = "O campo Nome permite no máximo 50 caracteres!")]
        [MaxLength(100)]
        [Required(ErrorMessage = "Informe seu Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Informe seu Sobrenome")]
        [MaxLength(100)]
        public string Sobrenome { get; set; }

        [MaxLength(100)]
        [Display(Name ="Endereço")]
        public string Endereco { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Informe o Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Informe sua Data de Nascimento")]
        [Display(Name ="Data de Nascimento")]
        public DateTime Nascimento { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage ="Informe uma Senha")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Informe uma senha com mais de 6 Caracteres")]
        public string Senha { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Informe uma Senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Informe uma senha com mais de 6 Caracteres")]
        [Display(Name ="Confirmar Senha")]
        [Compare(nameof(Senha),ErrorMessage ="As senhas devem ser Iguais")]
        public string ConfirmarSenha { get; set; }
    }
}
