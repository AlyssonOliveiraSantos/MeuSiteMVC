namespace MeuSiteMVC.Helper
{
    public interface IEmail
    {
        bool Enviar(string email, string assunto, string menssagem);

            
    }
}
