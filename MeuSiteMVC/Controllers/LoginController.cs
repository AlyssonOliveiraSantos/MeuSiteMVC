using MeuSiteMVC.Helper;
using MeuSiteMVC.Models;
using MeuSiteMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    public class LoginController: Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }



        public IActionResult index()
        {
            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index","Login");
        }



        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid) 
                {

                   UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario); 
                            return RedirectToAction("Index", "Home");
                        }
                            
                            TempData["MenssagemErro"] = $"Senha do usuário é inválida. Tente novamente.";

                    }
                    TempData["MenssagemErro"] = $"Uusário e/ou senha inválido(s). Por favor tente novamente.";

                }
                return View("Index", loginModel);

            }
            catch (Exception ex)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos alterar seu usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");

            }
        }

    }
}
