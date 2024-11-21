using MeuSiteMVC.Models;
using MeuSiteMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> _usuario = _usuarioRepositorio.BuscarTodos();
            return View(_usuario);
        }

        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id); 
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MenssagemSucesso"] = "Usuário apagado com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MenssagemErro"] = "Ops, não conseguimos apagar seu usuário";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos apagar seu usuário, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MenssagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");

                }
                return View(usuario);

            }
            catch (Exception ex)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {        
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        id = usuarioSemSenhaModel.id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Email = usuarioSemSenhaModel.Email,
                        Login = usuarioSemSenhaModel.Login,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MenssagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }
             return View("Editar", usuario);
            }
            catch (Exception ex)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos alterar seu usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}
