using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chama_o_var_api.Model
{
	[Table("ingressos")]
	public class Ingresso
	{
		public Ingresso(int torcedor, int evento)
		{
			this.torcedor = torcedor;
			this.evento = evento;
		}

        //
        [Key]
        public int id { get; set; }
        public int torcedor { get; set; }
		public int evento { get; set; }
    }
}

