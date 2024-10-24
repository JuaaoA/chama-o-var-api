using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chama_o_var_api.Model
{
    [Table("torcedores")]
    public class Torcedor
    {
        public Torcedor(string nome_completo, string cpf, string email, string telefone, DateTime nascimento,string senha, bool tecnico)
        {
            this.nome_completo = nome_completo ?? throw new ArgumentNullException();
            this.cpf = cpf ?? throw new ArgumentNullException();
            this.email = email ?? throw new ArgumentNullException();
            this.telefone = telefone ?? throw new ArgumentNullException();
            this.nascimento = nascimento;
            this.senha = senha;
            this.tecnico = tecnico;

            // Começar com o score no 1000
            this.score = 1000;
        }

        [Key]
        public int id { get; set; }
        public string nome_completo { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public int score { get; set; }
        public DateTime nascimento { get; set; }
        public string senha { get; set; }
        public bool tecnico { get; set; }
	}
}

