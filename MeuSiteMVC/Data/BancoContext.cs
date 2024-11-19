using MeuSiteMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuSiteMVC.Data
{
    public class BancoContext : DbContext 
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar maiúsculas para todos os nomes de tabelas e colunas automaticamente
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Define o nome da tabela em maiúsculas
                entity.SetTableName(entity.GetTableName().ToUpper());

                // Para cada propriedade (coluna), define o nome em maiúsculas
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToUpper());
                }
            }

            // Outras configurações adicionais, se necessário
            base.OnModelCreating(modelBuilder);
        }


    }
}
