using MeuSiteMVC.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace MeuSiteMVC.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public Sessao(IHttpContextAccessor httpAccessor) 
        {
            _httpAccessor = httpAccessor;
        }

        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpAccessor.HttpContext.Session.GetString("SessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario) ) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _httpAccessor.HttpContext.Session.SetString("SessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoDoUsuario()
        {
            _httpAccessor.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
    }
}
