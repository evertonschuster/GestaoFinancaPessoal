using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using GestaoFinancaPessoal.Data;

namespace GestaoFinancaPessoal.Controllers
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        public ApplicationDbContext Contexto { get; }
        public IHttpContextAccessor ContextAcessor { get; }
        public Session Session { get; }

        protected IDAO DAO { get; set; }

        public Controller(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor)
        {
            this.Contexto = (ApplicationDbContext)Contexto;
            this.ContextAcessor = contextAcessor;
            this.Session = new Session(contextAcessor);
            this.DAO = new DAO<MasterModel>(this);

            this.ViewBag.Salvo = false;
            this.ViewBag.Excluido = false;
            this.ViewBag.Alterado = false;

        }

        protected void AddModelError(Exception e)
        {
            
            var modelErrorException = e as ModelErrorException;
            if(e == null)
            {
                throw e;
            }
            foreach (var item in modelErrorException.GetModelErrors())
            {
                ModelState.AddModelError(item.Key, item.ErroMessage);
            }
        }

        [ResponseCache(Duration = 0, Location =ResponseCacheLocation.None, NoStore =true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
