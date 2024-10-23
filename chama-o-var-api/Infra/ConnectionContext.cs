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
        public DbSet<Tecnico> Tecnicos { get; set; }

        // TODO - CRIAR CONFIGS

        // BANCO DE DADOS
        string mysqlConnection = "Server=localhost;" +
            "Port=3306;" +
            "Database=chamaovartestes;" +
            "User=root;" +
            "Password=40028922";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(mysqlConnection);
        }
    }
}
