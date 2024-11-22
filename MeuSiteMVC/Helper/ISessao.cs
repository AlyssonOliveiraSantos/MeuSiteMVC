
using MeuSiteMVC.Models;

namespace MeuSiteMVC.Helper
{
    public interface ISessao
    {
        public void CriarSessaoDoUsuario(UsuarioModel usuario);
        public void RemoverSessaoDoUsuario();
        public UsuarioModel BuscarSessaoDoUsuario();

    }
}
