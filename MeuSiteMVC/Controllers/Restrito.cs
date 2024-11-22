using MeuSiteMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class Restrito : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
