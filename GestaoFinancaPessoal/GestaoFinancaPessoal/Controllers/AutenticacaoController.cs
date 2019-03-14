using System;
using GestaoFinacaPessoal.Models;
using GestaoFinacaPessoal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestaoFinancaPessoal.DAO;
using GestaoFinancaPessoal.Uteis.Security;
using GestaoFinancaPessoal.ViewModels;
using System.Security.Claims;

namespace GestaoFinancaPessoal.Controllers
{
    public class AutenticacaoController : Controller
    {
        public AutenticacaoController(FinancaContexto Contexto, IHttpContextAccessor contextAcessor) : base(Contexto, contextAcessor)
        {
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastroUsuarioViewModel cadastrarUsuario)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(cadastrarUsuario);
                }

                Usuario usuario = new Usuario
                {
                    Nome = cadastrarUsuario.Nome,
                    Sobrenome = cadastrarUsuario.Sobrenome,
                    Endereco = cadastrarUsuario.Endereco,
                    Email = cadastrarUsuario.Email,
                    Nascimento = cadastrarUsuario.Nascimento,
                    Senha = Hash.GerarHash(cadastrarUsuario.Senha)
                };

                UsuarioDAO usuarioDAO = new UsuarioDAO(DAO);

                usuarioDAO.Add(usuario);
                usuarioDAO.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            catch (Exception e)
            {
                this.AddModelError(e);
                return View(cadastrarUsuario);
            }
        }

        public IActionResult Login(string ReturnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            UsuarioDAO usuarioDAO = new UsuarioDAO(DAO);
            var usuario = usuarioDAO.getByEmail(new Usuario { Email = loginViewModel.Login });
            if (usuario.Count != 1)
            {
                ModelState.AddModelError(nameof(loginViewModel.Login), "E-Mail não encontrado");
                return View(loginViewModel);
            }

            if (usuario[0].Senha != Hash.GerarHash(loginViewModel.Senha))
            {
                ModelState.AddModelError(nameof(loginViewModel.Senha), "Senha inválida");
                return View(loginViewModel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,usuario[0].Nome),
                new Claim("Login",usuario[0].Email)
            }, "ApplicationCookie");


            //Request.GetOwinContext().Authentication.SignIn(identity);

            return View(loginViewModel.ReturnUrl);

            
        }


    }
}