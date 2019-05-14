using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinancaPessoal.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        public List<ReceitaDespesa> GetReceitaDespesa()
        {
            return null;
        }

        public IActionResult Index()
        {
            var contaDAO = this.DAO.NewDAO<ContaDAO>();
            var lancamentoDAO = this.DAO.NewDAO<LancamentoDAO>();
            var obj = new DashBoardInicial();
            obj.SaldoAtual = contaDAO.GetSaldoAtual();
            lancamentoDAO.GetValorTotalReceitaDespesa(obj);

            return View(obj);
        }

        [HttpPost]
        public IList<ReceitaDespesa> GetReceitaDespesasMes()
        {
            var lancamentoDAO = this.DAO.NewDAO<LancamentoDAO>();
            return lancamentoDAO.GetReceitaDespesasMes();
        }

        [HttpGet]
        public IList<CalendarEvent> GetCalendarEvent(DateTime start, DateTime end)
        {

            var lancamentoDAO = this.DAO.NewDAO<LancamentoDAO>();
            return lancamentoDAO.GetCalendarEvent(start, end);
        }
    }
}