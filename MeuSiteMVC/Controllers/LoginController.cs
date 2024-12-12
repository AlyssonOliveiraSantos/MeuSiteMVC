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
                    loginModel.Senha = loginModel.Senha.GerarHash();
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

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELOGIN(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();

                        TempData["MenssagemSucesso"] = "Enviamos para seu e-mail cadastrado uma nova senha.";
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MenssagemErro"] = "Não conseguimos redefinir sua senha. Por favor, verifique os dados informados";
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos redefinir sua senha. Tente novamente. Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        public IActionResult RedefinirSenha()
        {
            return View();
        }

    }
}
