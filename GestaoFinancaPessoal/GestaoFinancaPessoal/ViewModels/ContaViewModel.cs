using GestaoFinancaPessoal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.ViewModels
{
    public class ContaViewModel : Conta
    {
        public ContaViewModel(Conta conta, Boolean hasLancamento)
        {
            this.Id = conta.Id;
            this.Descricao = conta.Descricao;
            this.Nome = conta.Nome;
            this.Saldo = conta.Saldo;
            this.Tipo = conta.Tipo;
            //this.Banco = conta.Banco;
            this.DataAtualizacao = conta.DataAtualizacao;
            this.IsSuspensa = conta.IsSuspensa;
            this.HasLancamento = hasLancamento;
        }

        public Boolean HasLancamento { get; set; }
    }
}

