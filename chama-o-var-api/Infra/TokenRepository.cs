using System;
using chama_o_var_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chama_o_var_api.Infra
{
	public class TokenRepository : ITokenRepository
	{
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Token token)
        {
            _context.Tokens.Add(token);
            _context.SaveChanges();
        }

        public bool VerificarUnicidade(string codigo)
        {
            // Se já existir um token com esse nome
            if (_context.Tokens.FirstOrDefault(ct => ct.nome == codigo) != null) return false;

            // Se não
            return true;
        }

        public Token? GetToken(string name)
        {
            // Pegar o token usando um nome específico
            Token? tkEncontrado = _context.Tokens.SingleOrDefault(tk => tk.nome == name);

            // Retonar o token em si, ele pode ser nulo
            return tkEncontrado;
        }

        public Torcedor? GetTecnicoByToken(string token)
        {
            // Declarar o tecnico vazio
            Torcedor? tecnico_encontrado;

            // Tentar
            try
            {
                // Pegar o token
                Token? id_tecnico = _context.Tokens.SingleOrDefault(tk => tk.nome == token);

                // Retornar nulo, caso não encontre
                if (id_tecnico == null)
                {
                    return null;
                }

                // encontrar o técnico de acordo com o token encontrado
                tecnico_encontrado = _context.Torcedores.SingleOrDefault(user => user.id == id_tecnico.torcedor && user.tecnico);

                // Finalmente, retornar o tecnico
                return tecnico_encontrado;
            }
            catch
            {
                return null;
            }
        }

        public string CriarToken(int idTorcedor)
        {
            string codigoToken;

            // Criar um código do token até ele ser único 
            do
            {
                codigoToken = InputValidation.GenerateNewTokenString(20);
            }
            while (!this.VerificarUnicidade(codigoToken));

            // Adicionar token ao sistema
            var novoToken = new Token(codigoToken, idTorcedor);
            this.Add(novoToken);

            return codigoToken;
        }

        public List<Token> Get()
        {
            return _context.Tokens.ToList();
        }
    }
}

