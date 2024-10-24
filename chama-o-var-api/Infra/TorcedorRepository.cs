using System;
using chama_o_var_api.Model;

namespace chama_o_var_api.Infra
{
	public class TorcedorRepository : ITorcedorRepository
	{
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Torcedor torcedor)
        {
            _context.Torcedores.Add(torcedor);
            _context.SaveChanges();
        }

        public List<Torcedor> Get()
        {
            return _context.Torcedores.ToList();
        }
    }
}

