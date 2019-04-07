using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using GestaoFinancaPessoal.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.DAO
{
    public class ContaDAO : DAO.DAO<Conta>
    {
        public ContaDAO(IDAO dao) : base(dao)
        {
        }

        public ContaDAO(ApplicationDbContext context, Session session) : base(context, session)
        {
        }

        public override void Add(Conta conta)
        {
            if (this.GetContaByDescricao(conta).Count != 0)
            {
                ModelError erro = new ModelError(nameof(conta.Nome), "Conta já cadastrada.");
                throw new ModelErrorException(erro);
            }
            base.Add(conta);
        }


        public List<Conta> GetContaByDescricao(Conta conta)
        {
            var result = this.DbSet.Where(c => c.Nome == conta.Nome).ToList();
            return result;
        }

        public IList<ContaViewModel> ListContaView(Boolean mapeado = false)
        {
            if (mapeado)
            {
                return DbSet.Where(c => !c.IsSuspensa).Select(
                    c => new ContaViewModel(c, (Context.Lancamento.Where(l => l.Conta.Id == c.Id || l.ContaDestion.Id == c.Id).FirstOrDefault() != null)))
                   .ToList();
            }
            else
            {
                return DbSet.Where(c => !c.IsSuspensa).Select(
                    c => new ContaViewModel(c, (Context.Lancamento.Where(l => l.Conta.Id == c.Id || l.ContaDestion.Id == c.Id).FirstOrDefault() != null)))
                    .AsNoTracking().ToList();
            }
        }

        public IList<Conta> List(Boolean mapeado = false, Boolean comSuspensa = false )
        {
            if (mapeado)
            {
                return DbSet.Where(c => !c.IsSuspensa || c.IsSuspensa == comSuspensa ).ToList();//ele mapeia todos objetos 
            }
            else
            {
                return DbSet.Where(c => !c.IsSuspensa || c.IsSuspensa == comSuspensa).AsNoTracking().ToList();//ele mapeia todos objetos 

            }
        }
    }
}
