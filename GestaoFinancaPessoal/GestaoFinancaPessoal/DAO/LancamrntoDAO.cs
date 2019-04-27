using GestaoFinancaPessoal.Controllers;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using GestaoFinancaPessoal.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.DAO
{
    public class LancamentoDAO : DAO.DAO<Lancamento>
    {
        public LancamentoDAO(IDAO dao) : base(dao)
        {
        }

        public LancamentoDAO(Controller controller) : base(controller)
        {
        }

        public override Lancamento getById(int id)
        {
            return DbSet.Where(i => i.Id == id).Include(l => l.Conta).Include(l => l.ContaDestion).Include(l => l.Categoria).FirstOrDefault();
        }

        public override void Add(Lancamento l)
        {
            if (l.ContaDestion != null && l.Conta.Id == l.ContaDestion.Id)
            {
                this.ModelState.AddModelError(nameof(l.ContaDestion.Id), "Conta destino nao pode ser a mesma da origem.");
                return;
            }
            base.Add(l);
        }
        public virtual IList<Lancamento> List(VisualizarLancamentoViewModel v, Boolean mapeado = false)
        {

            //criacao do filtro
            Expression<Func<Lancamento, bool>> predicate =
                l => (l.Conta.Id == v.Conta.Id || v.Conta.Id == 0)
                    && (l.TipoLancamento == v.TipoLancamento || v.TipoLancamento == TipoLancamento.TODOS)
                    && (l.DataVencimento >= v.DataInicial || v.DataInicial == DateTime.MinValue)
                    && (l.DataVencimento <= v.DataFinal || v.DataFinal == DateTime.MinValue)
                    && ((l.DataVencimento <= DateTime.Now && l.IsPago == false) || v.PendenteVencidos == false)
                    && ((l.DataVencimento >= DateTime.Now && l.DataVencimento <= DateTime.Now.AddDays(30) && l.IsPago == false) || v.ProximoVencer == false);



            if (mapeado)
            {
                return DbSet.Include(l => l.Conta).Where(predicate).OrderByDescending(l => l.DataVencimento).ToList();//ele mapeia todos objetos 
            }
            else
            {
                return DbSet.AsNoTracking().Include(l => l.Conta).Where(predicate).OrderByDescending(l => l.DataVencimento).ToList();//ele nao mapeia os objetos 
            }
            
        }
    }
}
