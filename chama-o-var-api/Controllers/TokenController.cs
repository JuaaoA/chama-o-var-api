using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using chama_o_var_api.Model;
using chama_o_var_api.Infra;

namespace chama_o_var_api.Controllers
{
    [ApiController]
    [Route("chamaovar-api/torcedor/token")]
    public class TokenController : ControllerBase
	{
        // Interface
        private readonly ITokenRepository _tokenRepository;
        private readonly ITorcedorRepository _torcedorRepository;

        // Construtor
        public TokenController(ITokenRepository tokenRepository, ITorcedorRepository torcedorRepository)
		{
            this._tokenRepository = tokenRepository;
            this._torcedorRepository = torcedorRepository;
		}

        // Requests
        [HttpPost]
        public IActionResult CriarToken(int idTorcedor)
        {
            string codigoToken;

            // Criar um código do token até ele ser único 
            do
            {
                codigoToken = InputValidation.GenerateNewTokenString(20);
            }
            while (!_tokenRepository.VerificarUnicidade(codigoToken));

            // Adicionar token ao sistema
            var novoToken = new Token(codigoToken, idTorcedor);
            _tokenRepository.Add(novoToken);

            return Ok(codigoToken);
        }

        [HttpGet]
        public IActionResult GetTorcedorbyToken(string nome_token)
        {
            // Procurar o token do usuário
            Token? tk = _tokenRepository.GetToken(nome_token);

            // Caso o token não tenha sido encontrado
            if (tk == null)
            {
                return StatusCode(500, "O token expirou ou não foi encontrado");
            }

            // Encontrar dados do usuário usando o token
            Torcedor? torcedor = _torcedorRepository.GetTorcedorById(tk.torcedor);

            // Caso não tenha encontrado o torcedor
            if (torcedor == null)
            {
                // Reporte o erro ao front
                return StatusCode(500, "Torcedor não encontrado!");
            }

            // Por fim, enviar o torcedor ao front
            return StatusCode(200, torcedor);
        }
    }
}

