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
            return View(nameof(Index), contribuinteDAO.List());
        }


        // GET: CPFCNPJ/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CPFCNPJ/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CPFCNPJ contribuinte)
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
                if (!ModelState.IsValid)
                {
                    return View(contribuinte);
                }

                if (contribuinte.Id == 0)
                {
                    contribuinteDAO.Add(contribuinte);
                    ViewBag.Salvo = true;
                }
                else
                {
                    contribuinteDAO.Update (contribuinte);
                    ViewBag.Alterado = true;

                }

                contribuinteDAO.SaveChanges();

                return View(contribuinte);
            }
            catch (Exception e)
            {
                return View(contribuinte);
            }
        }

        // GET: CPFCNPJ/Edit/5
        public ActionResult Edit(int id)
        {
            var CPFCNPJDAO = this.DAO.NewDAO<CpfCnpjDAO>();
            return View("Create", CPFCNPJDAO.getById(id));
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
            var CPFCNPJDAO = this.DAO.NewDAO<CpfCnpjDAO>();
            CPFCNPJDAO.Remove(new CPFCNPJ { Id = id });
            CPFCNPJDAO.SaveChanges();
            ViewBag.Excluido = true;

            return Index();
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