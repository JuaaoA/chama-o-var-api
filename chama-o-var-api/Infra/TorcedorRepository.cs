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

        public string PossuiCredenciaisUnicas(string cpf, string email, string telefone)
        {
            if (_context.Torcedores.FirstOrDefault(ct => ct.cpf == cpf) != null) return "CPF";

            if (_context.Torcedores.FirstOrDefault(ct => ct.email == email) != null) return "Email";

            if (_context.Torcedores.FirstOrDefault(ct => ct.telefone == telefone) != null) return "Telefone";

            // Caso o usuário seja único
            return "";
        }

        public List<Torcedor> Get()
        {
            return _context.Torcedores.ToList();
        }
    }
}

