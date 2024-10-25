using System;

namespace chama_o_var_api.Model
{
	/*
	 * Essa interface dispõe as ações possíveis num repositório do TORCEDOR
	 * Assim, os dados podem ser acessados pelo controlador sem a necessidade
	 * de instanciar um contexto de conexão
	 */
	public interface ITorcedorRepository
	{
		void Add(Torcedor torcedor);

		string PossuiCredenciaisUnicas(string cpf, string email, string telefone);

		Torcedor? RealizarLogin(string email, string senha);

        List<Torcedor> Get();

		Torcedor? GetTorcedorById(int id);
    }
}

