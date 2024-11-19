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

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MenssagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");

                }
                return View(usuario);

            }
            catch (Exception ex)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos cadastrar seu usuario, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}
