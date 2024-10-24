using System;

namespace chama_o_var_api.Model
{
	public interface ITorcedorRepository
	{
		void Add(Torcedor torcedor);

		List<Torcedor> Get();
	}
}

