using MeuSiteMVC.Models;

namespace MeuSiteMVC.Repositorio
{
    public interface IContatoRepositorio
    {

        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contato);
 
    }
}
