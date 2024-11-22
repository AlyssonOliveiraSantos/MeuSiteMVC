using MeuSiteMVC.Filters;
using MeuSiteMVC.Models;
using MeuSiteMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<ContatoModel>_contatos = _contatoRepositorio.BuscarTodos();
            return View(_contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MenssagemSucesso"] = "Contato apagado com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MenssagemErro"] = "Ops, não conseguimos apagar seu contato";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos apagar seu contato, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MenssagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");

                }
                    return View(contato);
                
            }
            catch (Exception ex) {
                TempData["MenssagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MenssagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (Exception ex)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos alterar seu contato, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
