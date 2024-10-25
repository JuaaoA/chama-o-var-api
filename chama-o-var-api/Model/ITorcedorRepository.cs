using System;

namespace chama_o_var_api.Model
{
	public interface ITorcedorRepository
	{
		void Add(Torcedor torcedor);

		string PossuiCredenciaisUnicas(string cpf, string email, string telefone);

        List<Torcedor> Get();
	}
}

