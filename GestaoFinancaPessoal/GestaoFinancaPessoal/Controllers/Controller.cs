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
        protected ApplicationDbContext Contexto { get; }
        protected IHttpContextAccessor ContextAcessor { get; }
        protected Session Session { get; }

        protected IDAO DAO { get; set; }

        public Controller(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor)
        {
            this.Contexto = (ApplicationDbContext)Contexto;
            this.ContextAcessor = contextAcessor;
            this.Session = new Session(contextAcessor);
            this.DAO = new DAO<MasterModel>(this.Contexto, this.Session);

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
    }
}
