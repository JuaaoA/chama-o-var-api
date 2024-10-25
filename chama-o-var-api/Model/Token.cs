using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chama_o_var_api.Model
{
	[Table("tokens")]
	public class Token
	{
		public Token(string nome, int torcedor)
		{
			this.nome = nome ?? throw new ArgumentNullException();
			this.torcedor = torcedor;
		}

		[Key]
		public int id { get; set; }
		public string nome { get; set; }
		public int torcedor { get; set; }

	}
}

