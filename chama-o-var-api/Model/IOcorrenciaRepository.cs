using System;
namespace chama_o_var_api.Model
{
    /*
	 * Essa interface dispõe as ações possíveis num repositório das OCORRENCIAS
	 * Assim, os dados podem ser acessados pelo controlador sem a necessidade
	 * de instanciar um contexto de conexão
	 */
    public interface IOcorrenciaRepository
	{
		void Add(Ocorrencia ocorrencia);

		Ocorrencia? GetOneById(int id);

		List<Ocorrencia>? GetAllByTorcedorId(int torcedorId);

		bool Delete(int id);

		bool Update(int id, string acontecimento, DateTime data, int penalidade);

		List<Ocorrencia> Get();
	}
}

