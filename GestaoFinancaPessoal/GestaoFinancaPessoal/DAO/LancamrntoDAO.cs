using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.DAO
{
    public class LancamentoDAO : DAO.DAO<Lancamento>
    {
        public LancamentoDAO(IDAO dao) : base(dao)
        {
        }

        public LancamentoDAO(ApplicationDbContext context, Session session) : base(context, session)
        {
        }

        public override Lancamento getById(int id)
        {
            return DbSet.Where(i => i.Id == id).Include(l => l.Conta).Include(l => l.ContaDestion ).Include(l => l.Categoria).FirstOrDefault();
        }

        public override void Add(Lancamento l)
        {
            if(l.ContaDestion != null && l.Conta.Id == l.ContaDestion.Id)
            {
                ModelError erro = new ModelError(nameof(l.ContaDestion.Id), "Conta destino nao pode ser a mesma da origem.");
                throw new ModelErrorException(erro);
            }
            base.Add(l);
        }
    }
}
