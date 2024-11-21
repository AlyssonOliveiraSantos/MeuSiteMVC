using MeuSiteMVC.Models;
using MeuSiteMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    public class LoginController: Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        
        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult index()
        {
            return View();
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
                            return RedirectToAction("Index", "Home");
                            TempData["MenssagemErro"] = $"Senha do usuário é inválida. Tente novamente.";
                    }
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
