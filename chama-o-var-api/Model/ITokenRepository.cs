using System;

namespace chama_o_var_api.Model
{
    /*
	 * Essa interface dispõe as ações possíveis num repositório do TOKEN
	 * Assim, os dados podem ser acessados pelo controlador sem a necessidade
	 * de instanciar um contexto de conexão
	 */
    public interface ITokenRepository
    {
        void Add(Token token);

        bool VerificarUnicidade(string codigo);

        Token? GetToken(string name);

        string CriarToken(int idTorcedor);

        List<Token> Get();
    }
}

