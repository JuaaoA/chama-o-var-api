﻿using System;
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

        public Torcedor? GetTorcedorById(int id)
        {
            // Declarar um torcedor nulo
            Torcedor? usuario = null;

            // Tentar encontrar
            try
            {
                // Procurar usuário
                usuario = _context.Torcedores.SingleOrDefault(usr => usr.id == id);
            }
            catch
            {
                // Retorne nulo caso erro
                return null;
            }

            // Retonar o usuário usando esse id
            return usuario;
        }

        public Torcedor? RealizarLogin(string email, string senha)
        {
            // Começar com o valor nulo
            Torcedor? usuario = null;

            // Tentar
            try
            {
                // Procurar o torcedor
                usuario = _context.Torcedores.SingleOrDefault(user =>
                user.email == email && user.senha == senha);
            }
            catch
            {
                // Retornar nulo caso de erro
                return null;
            }

            // Caso tudo dê certo, retorne o usuário encontrado
            return usuario;
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

