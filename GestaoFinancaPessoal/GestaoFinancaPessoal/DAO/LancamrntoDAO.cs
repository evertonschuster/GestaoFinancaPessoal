using GestaoFinancaPessoal.Controllers;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using GestaoFinancaPessoal.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            if (l.TipoLancamento == TipoLancamento.TRANSFERENCIA && l.ContaDestion == null)
            {
                this.ModelState.AddModelError(nameof(l.ContaDestion.Id), "Informe a Conta destino.");
                throw new ModelErrorException();
            }

            if (l.Valor == 0)
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

        public IList<ReceitaDespesa> GetReceitaDespesasAno()
        {
            VereficarLancamentosPendentes();

            VisualizarLancamentoViewModel v = new VisualizarLancamentoViewModel();
            v.DataFinal = DateTime.Now;
            v.DataInicial = DateTime.Now.AddYears(-1);


            //criacao do filtro
            Expression<Func<Lancamento, bool>> predicate =
                l => (l.Conta.Id == v.Conta.Id || v.Conta.Id == 0)
                    && (l.DataVencimento <= v.DataFinal || v.DataFinal == DateTime.MinValue)
                    && (l.DataVencimento >= v.DataInicial || v.DataInicial == DateTime.MinValue)
                    && l.TipoLancamento != TipoLancamento.TRANSFERENCIA;


            var result = DbSet.AsNoTracking().Include(l => l.Conta)
                  .Where(predicate)
                  .GroupBy(l => new { l.DataVencimento.Year, l.DataVencimento.Month, l.TipoLancamento })
                  .OrderByDescending(l => l.Key.Month)
                  .Select(s => new
                  {
                      ValorLancamento = s.Sum(i => i.Valor),
                      TipoLancamento = s.Key.TipoLancamento,
                      DataLancamento = s.Key.Month,
                      AnoLancamento = s.Key.Year
                  })
                  .Take(24)
                  .ToList();//ele nao mapeia os objetos 

            //precissa agrupar por mes, para nao precisar faz isso na view

            var lstMonth = result
                .OrderBy(l => l.DataLancamento)
                .Select(r => r.DataLancamento)
                .Distinct()
                .ToList();
            var listReceitaDespesa = new List<ReceitaDespesa>();

            //precissa agrupar por mes, para nao precisar faz isso na view
            for (int i = 0; i <= 12; i++)
            {
                var receitaDespesa = new ReceitaDespesa();
                receitaDespesa.DataLancamento = DateTimeFormatInfo.CurrentInfo.GetMonthName(v.DataInicial.AddMonths(i).Month);
                receitaDespesa.AnoMesLancamento = v.DataInicial.AddMonths(i).ToString("yyyy-MM");
                receitaDespesa.DescricaoDespensa = "Despesa.";
                receitaDespesa.DescricaoReceita = "Receita.";

                var lancamentoMes = result.Where(l => l.DataLancamento == v.DataInicial.AddMonths(i).Month).ToList();
                var lancamentoReceita = lancamentoMes.Where(l => l.TipoLancamento == TipoLancamento.RECEITA).ToList();
                receitaDespesa.ValorReceita = (lancamentoReceita.Count == 0) ? 0 : lancamentoReceita[0].ValorLancamento;

                var lancamentoDespesa = lancamentoMes.Where(l => l.TipoLancamento == TipoLancamento.DESPESA).ToList();
                receitaDespesa.ValorDespesa = (lancamentoDespesa.Count == 0) ? 0 : lancamentoDespesa[0].ValorLancamento;

                listReceitaDespesa.Add(receitaDespesa);
            }


            return listReceitaDespesa;
        }

        public IList<ReceitaDespesa> GetReceitaDespesasMes()
        {
            VereficarLancamentosPendentes();

            VisualizarLancamentoViewModel v = new VisualizarLancamentoViewModel();
            v.DataFinal = DateTime.Now;
            v.DataInicial = DateTime.Now.AddMonths(-5);


            //criacao do filtro
            Expression<Func<Lancamento, bool>> predicate =
                l => (l.Conta.Id == v.Conta.Id || v.Conta.Id == 0)
                    && (l.DataVencimento <= v.DataFinal || v.DataFinal == DateTime.MinValue)
                    && (l.DataVencimento >= v.DataInicial || v.DataInicial == DateTime.MinValue)
                    && l.TipoLancamento != TipoLancamento.TRANSFERENCIA;


            var result = DbSet.AsNoTracking().Include(l => l.Conta)
                  .Where(predicate)
                  .GroupBy(l => new { l.DataVencimento.Month, l.DataVencimento.Year, l.TipoLancamento })
                  .OrderByDescending(l => l.Key.Month)
                  .Select(s => new
                  {
                      ValorLancamento = s.Sum(i => i.Valor),
                      TipoLancamento = s.Key.TipoLancamento,
                      DataLancamento = s.Key.Month,
                      AnoLancamento = s.Key.Year
                  })
                  .Take(10)
                  .ToList();//ele nao mapeia os objetos 

            //precissa agrupar por mes, para nao precisar faz isso na view

            var lstMonth = result
                .OrderBy(l => l.DataLancamento)
                .Select(r => r.DataLancamento)
                .Distinct()
                .ToList();
            var listReceitaDespesa = new List<ReceitaDespesa>();

            //precissa agrupar por mes, para nao precisar faz isso na view
            for (int i = 0; i <= 5; i++)
            {
                var receitaDespesa = new ReceitaDespesa();
                receitaDespesa.DataLancamento = DateTimeFormatInfo.CurrentInfo.GetMonthName(v.DataInicial.AddMonths(i).Month);
                receitaDespesa.AnoMesLancamento = v.DataInicial.AddDays(i).ToString("MM/yyyy");
                receitaDespesa.DescricaoDespensa = "Despesa.";
                receitaDespesa.DescricaoReceita = "Receita.";

                var lancamentoMes = result.Where(l => l.DataLancamento == v.DataInicial.AddMonths(i).Month).ToList();
                var lancamentoReceita = lancamentoMes.Where(l => l.TipoLancamento == TipoLancamento.RECEITA).ToList();
                receitaDespesa.ValorReceita = (lancamentoReceita.Count == 0) ? 0 : lancamentoReceita[0].ValorLancamento;

                var lancamentoDespesa = lancamentoMes.Where(l => l.TipoLancamento == TipoLancamento.DESPESA).ToList();
                receitaDespesa.ValorDespesa = (lancamentoDespesa.Count == 0) ? 0 : lancamentoDespesa[0].ValorLancamento;

                listReceitaDespesa.Add(receitaDespesa);
            }


            return listReceitaDespesa;
        }

        public IList<ReceitaDespesa> GetReceitaDespesasDia()
        {
            VereficarLancamentosPendentes();

            VisualizarLancamentoViewModel v = new VisualizarLancamentoViewModel();
            v.DataFinal = DateTime.Now;
            v.DataInicial = DateTime.Now.AddDays(-5);


            //criacao do filtro
            Expression<Func<Lancamento, bool>> predicate =
                l => (l.Conta.Id == v.Conta.Id || v.Conta.Id == 0)
                    && (l.DataVencimento <= v.DataFinal || v.DataFinal == DateTime.MinValue)
                    && (l.DataVencimento >= v.DataInicial || v.DataInicial == DateTime.MinValue)
                    && l.TipoLancamento != TipoLancamento.TRANSFERENCIA;


            var result = DbSet.AsNoTracking().Include(l => l.Conta)
                  .Where(predicate)
                  .GroupBy(l => new { l.DataVencimento.Day, l.DataVencimento.Month, l.DataVencimento.Year, l.TipoLancamento })
                  .OrderByDescending(l => l.Key.Day)
                  .Select(s => new
                  {
                      ValorLancamento = s.Sum(i => i.Valor),
                      TipoLancamento = s.Key.TipoLancamento,
                      DataLancamento = s.Key.Month,
                      AnoLancamento = s.Key.Year,
                      DiaLancamento = s.Key.Day
                  })
                  .Take(10)
                  .ToList();

            //precissa agrupar por mes, para nao precisar faz isso na view
            var listReceitaDespesa = new List<ReceitaDespesa>();
            for (int i = 0; i <= 5; i++)
            {
                var receitaDespesa = new ReceitaDespesa();
                receitaDespesa.DataLancamento = v.DataInicial.AddDays(i).ToString("dd/MM/yyyy");
                receitaDespesa.AnoMesLancamento = v.DataInicial.AddDays(i ).ToString("dd/MM/yyyy");
                receitaDespesa.DescricaoDespensa = "Despesa.";
                receitaDespesa.DescricaoReceita = "Receita.";

                var lancamentoDia = result.Where(l => l.DiaLancamento == v.DataInicial.AddDays(i ).Day).ToList();
                var lancamentoReceita = lancamentoDia.Where(l => l.TipoLancamento == TipoLancamento.RECEITA ).ToList();
                receitaDespesa.ValorReceita = (lancamentoReceita.Count == 0) ? 0 : lancamentoReceita[0].ValorLancamento;

                var lancamentoDespesa = lancamentoDia.Where(l => l.TipoLancamento == TipoLancamento.DESPESA).ToList();
                receitaDespesa.ValorDespesa = (lancamentoDespesa.Count == 0) ? 0 : lancamentoDespesa[0].ValorLancamento;

                listReceitaDespesa.Add(receitaDespesa);
            }


            return listReceitaDespesa;
        }

        public IList<CalendarEvent> GetCalendarEvent(DateTime start, DateTime end)
        {
            VereficarLancamentosPendentes();

            return DbSet.Where(l => l.DataVencimento >= start && l.DataVencimento <= end)
                .Select(l => new CalendarEvent
                {
                    Id = l.Id,
                    Title = l.Descricao,
                    Start = l.DataVencimento,

                    ClassName = l.TipoLancamento == TipoLancamento.DESPESA ? "bg-danger" : l.TipoLancamento == TipoLancamento.RECEITA ? "bg-success" : "bg-info",
                    TextColor = "black",
                    AllDay = true,
                    Valor = l.Valor,
                    ValorPago = l.ValorPago,
                    DsConta = "Conta"
                })
                .ToList();
        }

        public void GetValorTotalReceitaDespesa(DashBoardInicial dashBoard)
        {

            dashBoard.TotalDespesaMes = DbSet.Where(c => c.TipoLancamento == TipoLancamento.DESPESA).Sum(c => c.Valor);
            dashBoard.TotalReceitaaMes = DbSet.Where(c => c.TipoLancamento == TipoLancamento.RECEITA).Sum(c => c.Valor);

        }
    }
}







