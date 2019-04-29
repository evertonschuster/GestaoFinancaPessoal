using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoFinancaPessoal.Models;
using Microsoft.AspNetCore.Authorization;
using GestaoFinancaPessoal.Data;
using Microsoft.AspNetCore.Http;
using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.ViewModels;

namespace GestaoFinancaPessoal.Controllers
{

    //[AllowAnonymous]
    public class HomeController : Controller
    {
        public HomeController(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        public static Notificacao TempoNotificacao { get; set; } = new Notificacao();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public string NotificacaoEdit([FromBody] Notificacao notificacao)
        {
            try
            {
                HomeController.TempoNotificacao  = notificacao;
                return "Atualizado com Sucesso.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        [HttpGet]
        public Notificacao NotificacaoTempoGet()
        {
            return HomeController.TempoNotificacao;

        }

        [HttpGet]
        public IList<NotificacaoViewModel> NotificacaoGet()
        {
            var lancamentoDAO = this.DAO.NewDAO<LancamentoDAO>();
            
            var ListNot = lancamentoDAO.GetNotificacao(HomeController.TempoNotificacao);

            return ListNot;
        }
    }
}
