using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;

namespace GestaoFinancaPessoal.Controllers
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        protected FinancaContexto Contexto { get; }
        protected IHttpContextAccessor ContextAcessor { get; }
        protected Session Session { get; }

        protected IDAO DAO { get; set; }

        public Controller(DbContext Contexto, IHttpContextAccessor contextAcessor)
        {
            this.Contexto = (FinancaContexto)Contexto;
            this.ContextAcessor = contextAcessor;
            this.Session = new Session(contextAcessor);
            this.DAO = new DAO<MasterModel>(this.Contexto, this.Session);

            //Console.WriteLine("Retstet");
            //StackTrace stackTrace = new StackTrace();
            //var classe = stackTrace.GetFrame(2).GetMethod().DeclaringType;

            //Localiza os prop com anotacao
            //List<PropertyInfo> parametros = new List<PropertyInfo>();
            //PropertyInfo[] properties = classe.GetProperties();
            //foreach (var propertyInfo in properties)
            //{
            //    var atributo = propertyInfo.GetCustomAttribute<ParameterSystemAttribute>();
            //    if (atributo is ParameterSystemAttribute)
            //    {
            //        parametros.Add(propertyInfo);
            //        { System.Collections.ObjectModel.ReadOnlyCollection`1[System.Reflection.CustomAttributeData]}
            //    }

            //}

            Console.WriteLine("Final do re");

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
