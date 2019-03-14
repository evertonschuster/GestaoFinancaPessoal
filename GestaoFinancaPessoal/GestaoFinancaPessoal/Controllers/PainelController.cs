using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoFinancaPessoal.Controllers
{
    [Authorize]
    public class PainelController : Controller
    {
        public PainelController(DbContext Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        
        public IActionResult Index()
        {
            return View();
        }
    }
}