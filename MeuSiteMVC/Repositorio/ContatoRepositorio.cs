using MeuSiteMVC.Data;
using MeuSiteMVC.Models;
using System.Linq;

namespace MeuSiteMVC.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.id == id);
        }  

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = ListarPorId(contato.id);

            if (contatoDb == null) throw new Exception("Houve um erro na atualização");

            contatoDb.Nome = contato.Nome;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;

            _bancoContext.Contatos.Update(contatoDb);
            _bancoContext.SaveChanges();

            return contatoDb;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDb = ListarPorId(id);
            if (contatoDb == null) throw new Exception("Houve um erro na deleção desse contato");
            
            _bancoContext.Contatos.Remove(contatoDb);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
