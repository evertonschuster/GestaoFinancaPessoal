using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Models
{
    public class CPFCNPJ : MasterModel
    {
        [DataMember]
        [Required(ErrorMessage = "Informe o Tipo de Pessoa.")]
        [Display(Name = "Tipo de Pessoa")]
        public TipoPessoa TipoPessoa { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe CPF/CNPJ.")]
        [Display(Name = "CPF/CNPJ")]
        [StringLength(18,MinimumLength = 11,ErrorMessage = "O campo deve ter no mínimo 11 caracteres e no máximo 15.")]
        public string CpfCnpj { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Nome.")]
        [Display(Name = "Nome")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres e no máximo 100.")]
        public string Nome { get; set; }

        [DataMember]
        //[Required(ErrorMessage = "Informe o Nome do Contato.",AllowEmptyStrings = true )]
        [Display(Name = "Nome do Contato")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres e no máximo 100.")]
        public String NomeContato { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o RG.", AllowEmptyStrings = true )]
        [Display(Name = "RG")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "O campo deve ter no mínimo 11 caracteres e no máximo 13.")]
        public string RG { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Email.")]
        [Display(Name = "Email")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        public string Email { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Telefone.")]
        [Display(Name = "Telefone")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]

        public string Telefones { get; set; }

        //[DataMember]
        //[Required(ErrorMessage = "Informe Endereço.")]
        //[Display(Name = "Endereço")]
        //public string Endereco { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe a Rua.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Numero.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Complemento.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o Bairro.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe a Cidade.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe Estado.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe o CEP.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Informe a Observação.")]
        [StringLength(256, ErrorMessage = "O campo deve ter no máximo 256.")]
        [Display(Name = "Observações.")]
        public string Observacao { get; set; }


    }

    public enum TipoPessoa
    {
        FISICA = 0,
        JURIDICA = 1
    }
}
