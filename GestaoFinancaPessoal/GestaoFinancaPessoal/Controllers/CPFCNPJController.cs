using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinancaPessoal.Controllers
{
    public class CPFCNPJController : Controller
    {
        public CPFCNPJController(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        // GET: CPFCNPJ
        public ActionResult Index()
        {
            var contribuinteDAO = this.DAO.NewDAO<CpfCnpjDAO>();
            return View(contribuinteDAO.List());
        }


        // GET: CPFCNPJ/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CPFCNPJ/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CPFCNPJ contribuinte )
        {
            try
            {
                ViewBag.Salvo = false;
                if (contribuinte.TipoPessoa == TipoPessoa.FISICA)
                {
                    ModelState.Remove(nameof(contribuinte.NomeContato));
                }
                else
                {
                    ModelState.Remove(nameof(contribuinte.RG));
                }

                var contribuinteDAO = this.DAO.NewDAO<CpfCnpjDAO>();
                if (!ModelState.IsValid )
                {
                    return View (contribuinte);
                }

                contribuinteDAO.Add(contribuinte);
                contribuinteDAO.SaveChanges();
                ViewBag.Salvo = true;

                return View(contribuinte);
            }
            catch(Exception e)
            {
                return View(contribuinte);
            }
        }

        // GET: CPFCNPJ/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CPFCNPJ/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CPFCNPJ/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CPFCNPJ/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}