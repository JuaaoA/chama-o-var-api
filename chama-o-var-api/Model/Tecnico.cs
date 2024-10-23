using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chama_o_var_api.Model
{
    [Table("tecnicos")]
    public class Tecnico
    {
        public Tecnico(string nome, string cpf)
        {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.cpf = cpf;
        }

        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }

    }
}
