using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GestaoFinancaPessoal.Controllers
{
    public class ContaController : Controller
    {
        public ContaController(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        // GET: Conta
        public ActionResult Index()
        {
            var contaDAO = new ContaDAO(this.DAO);
            return View(contaDAO.ListContaView());
        }

        // GET: Conta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Conta/Create
        public ActionResult Create()
        {
            return View(new Conta { Saldo = 0 });
        }

        // POST: Conta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Conta conta)
        {
            ViewBag.Salvo = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var contaDAO = new ContaDAO(this.DAO);

                    conta.DataAtualizacao = DateTime.Now;
                    contaDAO.Add(conta);
                    contaDAO.SaveChanges();
                    ViewBag.Salvo = true;
                }
                else
                {
                    return View(conta);
                }

                this.ViewBag.Salvo = true;
                return View(conta);
            }
            catch (ModelErrorException e)
            {
                this.AddModelError(e);
                return View(conta);
            }

        }

        // GET: Conta/Edit/5
        public ActionResult Edit(int id)
        {
            var contaDAO = new ContaDAO(this.DAO);
            var conta = contaDAO.getById(id);

            return View(conta);
        }

        // POST: Conta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Conta conta)
        {
            try
            {
                ViewBag.Alterado = false;

                if (ModelState.IsValid)
                {
                    var contaDAO = new ContaDAO(this.DAO);

                    conta.Id = id;
                    conta.DataAtualizacao = DateTime.Now;
                    contaDAO.Update(conta);
                    contaDAO.SaveChanges();
                    ViewBag.Alterado = true;
                }
                else
                {
                    return View(conta);
                }

                return View(conta);
            }
            catch (ModelErrorException e)
            {
                this.AddModelError(e);
                return View(conta);
            }

        }

        // GET: Conta/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                ViewBag.Excluido = false;

                var contaDAO = new ContaDAO(this.DAO);
                Conta conta = contaDAO.getById(id);

                contaDAO.Remove(conta);
                contaDAO.SaveChanges();
                ViewBag.Excluido = true;
                return View("index", contaDAO.ListContaView());

            }
            catch (ModelErrorException e)
            {
                this.AddModelError(e);
            }
            return View();
        }

        public ActionResult Suspender(int id)
        {
            try
            {
                ViewBag.Suspenso = false;

                var contaDAO = new ContaDAO(this.DAO);
                Conta conta = contaDAO.getById(id);
                conta.IsSuspensa = true;
                contaDAO.Update(conta);
                contaDAO.SaveChanges();
                ViewBag.Suspenso = true;
                return View("index", contaDAO.ListContaView());

            }
            catch (ModelErrorException e)
            {
                this.AddModelError(e);
            }
            return View();
        }

        // POST: Conta/Delete/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IList<Conta> ListConta()
        {
            var contaDAO = new ContaDAO(this.DAO);
            return contaDAO.List();
        }
    }
}