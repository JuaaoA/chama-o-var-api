using System.Data;
using chama_o_var_api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Security;

namespace chama_o_var_api.Infra
{
    public class ConnectionContext : DbContext
    {
        // SETS
        public DbSet<Torcedor> Torcedores { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }

        // BANCO DE DADOS
        string mysqlConnection = "Server=localhost;" +
            "Port=3306;" +
            "Database=dbFuncionarios;" +
            "User=root;";

        // Ao configurar
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Usar MySQL
            optionsBuilder.UseMySQL(mysqlConnection);
        }
    }
}
