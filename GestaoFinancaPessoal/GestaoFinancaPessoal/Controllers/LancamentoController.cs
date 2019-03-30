using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Models;
using GestaoFinancaPessoal.Uteis.Exception.ModelErrorException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFinancaPessoal.Controllers
{
    public class LancamentoController : Controller
    {
        public LancamentoController(ApplicationDbContext Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        // GET: LancamentoReceita
        public ActionResult Index()
        {
            var lancamentoDAO = new LancamentoDAO(this.DAO);
            var listLancamento = lancamentoDAO.List();
            return View(listLancamento);
        }

        // GET: Receita
        public ActionResult Receita()
        {
            var categoriaDAO = new CategoriaDAO(this.DAO);
            var contaDAO = new ContaDAO(this.DAO);
            ViewBag.Categoria = categoriaDAO.List();
            ViewBag.Conta = contaDAO.List();


            return View(nameof(Create), new Lancamento { Tipo = TipoLancamento.RECEITA, DataPagamento = DateTime.Now, DataVencimento = DateTime.Now });
        }

        // GET: Receita
        public ActionResult Transferencia()
        {
            var categoriaDAO = new CategoriaDAO(this.DAO);
            var contaDAO = new ContaDAO(this.DAO);
            ViewBag.Categoria = categoriaDAO.List();
            ViewBag.Conta = contaDAO.List();


            return View(nameof(Create), new Lancamento { Tipo = TipoLancamento.TRANSFERENCIA, DataPagamento = DateTime.Now, DataVencimento = DateTime.Now });
        }

        // GET: Lancamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lancamento/Create
        public ActionResult Create()
        {
            var categoriaDAO = new CategoriaDAO(this.DAO);
            var contaDAO = new ContaDAO(this.DAO);
            ViewBag.Categoria = categoriaDAO.List();
            ViewBag.Conta = contaDAO.List();

            return View();
        }

        // POST: Lancamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lancamento lancamento)
        {
            try
            {
                var lancamentoDAO = new LancamentoDAO(this.DAO);
                var categoriaDAO = new CategoriaDAO(this.DAO);
                var contaDAO = new ContaDAO(this.DAO);
                ViewBag.Categoria = categoriaDAO.List();
                ViewBag.Conta = contaDAO.List();

                ModelState.Remove("Categoria.Nome");
                ModelState.Remove("Conta.Nome");
                ModelState.Remove("Conta.Tipo");
                ModelState.Remove("ContaDestion.Nome");
                ModelState.Remove("ContaDestion.Tipo");

                if (!ModelState.IsValid)
                {
                    return View(lancamento);
                }

                if (lancamento.Tipo != TipoLancamento.TRANSFERENCIA)
                {
                    lancamento.ContaDestion = null;
                }
                else
                if (lancamento.Conta.Id == lancamento.ContaDestion.Id)
                {
                    ModelError erro = new ModelError(nameof(lancamento.ContaDestion.Id), "Conta destino nao pode ser a mesma da origem.");
                    throw new ModelErrorException(erro);
                }

                if (lancamento.IsPago)
                {
                    //var conta = ((List<Conta>)(ViewBag.Conta)).Where(c => c.Id == lancamento.Conta.Id).FirstOrDefault();
                    var conta = contaDAO.getById(lancamento.Conta.Id);

                    if (lancamento.Tipo == TipoLancamento.DESPESA || lancamento.Tipo == TipoLancamento.TRANSFERENCIA)
                    {
                        conta.Saldo -= Convert.ToDouble(lancamento.ValorPago);
                    }

                    if (lancamento.Tipo == TipoLancamento.RECEITA)
                    {
                        conta.Saldo += Convert.ToDouble(lancamento.ValorPago);
                    }
                    contaDAO.Update(conta);

                    if (lancamento.Tipo == TipoLancamento.TRANSFERENCIA)
                    {
                        conta = contaDAO.getById(lancamento.ContaDestion.Id);
                        conta.Saldo += Convert.ToDouble(lancamento.ValorPago);
                        contaDAO.Update(conta);
                    }
                }

                lancamento.DataInclusao = DateTime.Now;
                lancamentoDAO.Attach(lancamento);
                lancamentoDAO.Add(lancamento);

                lancamentoDAO.SaveChanges();

                ViewBag.Salvo = true;
                return View(lancamento);
            }
            catch (ModelErrorException e)
            {
                this.AddModelError(e);
                return View(lancamento);
            }
        }

        // GET: Lancamento/Edit/5
        public ActionResult Edit(int id)
        {
            var categoriaDAO = new CategoriaDAO(this.DAO);
            var contaDAO = new ContaDAO(this.DAO);
            var lancamentoDAO = new LancamentoDAO(this.DAO);
            ViewBag.Categoria = categoriaDAO.List();
            ViewBag.Conta = contaDAO.List();

            var lancamento = lancamentoDAO.getById(id);
            return View(lancamento);
        }

        // POST: Lancamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Lancamento lancamento)
        {
            try
            {
                var lancamentoDAO = new LancamentoDAO(this.DAO);
                var categoriaDAO = new CategoriaDAO(this.DAO);
                var contaDAO = new ContaDAO(this.DAO);
                ViewBag.Categoria = categoriaDAO.List();
                ViewBag.Conta = contaDAO.List();

                ModelState.Remove("Categoria.Nome");
                ModelState.Remove("Conta.Nome");
                ModelState.Remove("Conta.Tipo");
                ModelState.Remove("ContaDestion.Nome");
                ModelState.Remove("ContaDestion.Tipo");
                if (!ModelState.IsValid)
                {
                    return View(lancamento);
                }

                if (lancamento.Tipo != TipoLancamento.TRANSFERENCIA)
                {
                    lancamento.ContaDestion = null;
                }
                else
                if (lancamento.Conta.Id == lancamento.ContaDestion.Id)
                {
                    ModelError erro = new ModelError("lancamento.ContaDestion.Id", "Conta destino nao pode ser a mesma da origem.");
                    throw new ModelErrorException(erro);
                }
                lancamento.DataInclusao = DateTime.Now;

                lancamentoDAO.Attach(lancamento);
                lancamentoDAO.Update(lancamento);
                lancamentoDAO.SaveChanges();

                ViewBag.Alterado = true;
                return View(lancamento);
            }
            catch (ModelErrorException e)
            {
                this.AddModelError(e);
                return View(lancamento);
            }
        }

        // GET: Lancamento/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                ViewBag.Excluido = false;

                var lancamentoDAO = new LancamentoDAO(this.DAO);
                var lancamento = lancamentoDAO.getById(id);
                lancamentoDAO.Remove(lancamento);
                lancamentoDAO.SaveChanges();

                var listLancamento = lancamentoDAO.List();
                ViewBag.Excluido = true;
                return View("index", listLancamento);

            }
            catch (ModelErrorException e)
            {
                this.AddModelError(e);
            }
            return View();
        }

        // POST: Lancamento/Delete/5
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