using GestaoFinancaPessoal.Controllers;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using GestaoFinancaPessoal.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
                throw new ModelErrorException();
            }

            if(l.TipoLancamento == TipoLancamento.TRANSFERENCIA && l.ContaDestion == null)
            {
                this.ModelState.AddModelError(nameof(l.ContaDestion.Id), "Informe a Conta destino.");
                throw new ModelErrorException();
            }

            if(l.Valor == 0)
            {
                this.ModelState.AddModelError(nameof(l.Valor), "O valor não foi informado.");
                throw new ModelErrorException();
            }

            if (l.DataNotificacao != null)
            {
                l.DataNotificacao = l.DataVencimento.Add(l.DataInicio * -1);
            }
            else
            {
                l.DataNotificacao = l.DataVencimento;
            }

            base.Add(l);
        }

        public override void Update(Lancamento l)
        {
            if (this.IsValid(l))
            {
                if (l.DataNotificacao != null)
                {
                    l.DataNotificacao = l.DataVencimento.Add(l.DataInicio * -1);
                }
                else
                {
                    l.DataNotificacao = l.DataVencimento;
                }

                DbSet.Update(l);
            }
        }

        public virtual IList<Lancamento> List(VisualizarLancamentoViewModel v, Boolean mapeado = false)
        {
            VereficarLancamentosPendentes();
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

        public void VereficarLancamentosPendentes()
        {
            var lancamentosBaixa = DbSet.Where(l => l.IsPago == false && l.DataPagamento <= DateTime.Now).ToList();
            foreach (var item in lancamentosBaixa)
            {
                item.IsPago = true;
            }

            this.SaveChanges();
        }

        public IList<NotificacaoViewModel> GetNotificacao(Notificacao n)
        {
            return DbSet.Where(l => l.DataNotificacao <= n.DataInicio && l.IsPago == false).Select(l => new NotificacaoViewModel()
            {
                Descricao = l.Descricao,
                DataVence = l.DataVencimento,
                TipoLancamento = l.TipoLancamento,
                Id = l.Id
            }).ToList();
        }
    }
}
