using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chama_o_var_api.Model
{
	[Table("eventos")]
	public class Evento
	{
		public Evento(string nome, DateTime data_evento, string detalhes, int minimo_pontuacao, int criador)
		{
			this.nome = nome ?? throw new ArgumentNullException("Nome nulo");
			this.data_evento = data_evento;
			this.detalhes = detalhes ?? throw new ArgumentNullException("Detalhes nulo");
			this.minimo_pontuacao = minimo_pontuacao;
			this.criador = criador;
		}

		//
		[Key]
		public int id { get; set; }
		public string nome { get; set; }
		public DateTime data_evento { get; set; }
		public string detalhes { get; set; }
		public int minimo_pontuacao { get; set; }
		public int criador { get; set; }
	}
}

