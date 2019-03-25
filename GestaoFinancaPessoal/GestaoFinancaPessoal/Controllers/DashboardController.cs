using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoFinancaPessoal.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinancaPessoal.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}