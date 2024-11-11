using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chama_o_var_api.Model
{
	[Table("ocorrencias")]
	public class Ocorrencia
	{
		public Ocorrencia(string acontecimento, DateTime data_ocorrencia,
			int penalidade, int torcedor, int colaborador)
		{
			this.acontecimento = acontecimento ?? throw new ArgumentNullException();
			this.data_ocorrencia = data_ocorrencia;
			this.penalidade = penalidade;
			this.torcedor = torcedor;
			this.colaborador = colaborador;
		}

		//
		[Key]
		public int id { get; set; }
		public string acontecimento { get; set; }
		public DateTime data_ocorrencia { get; set; }
		public int penalidade { get; set; }
		public int torcedor { get; set; }
		public int colaborador { get; set; }
    }
}

