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
    [Route("chamaovar-api/colaborador/login")]
    public class ColaboradorLogin : ControllerBase
    {
        // Interface
        private readonly ITorcedorRepository _torcedorRepository;
        private readonly ITokenRepository _tokenRepository;

        // Construtor
        public ColaboradorLogin(ITorcedorRepository torcedorRepository, ITokenRepository tokenRepository)
        {
            _torcedorRepository = torcedorRepository;
            _tokenRepository = tokenRepository;
        }

        // Requests
        [HttpGet]
        public IActionResult TentarLogin(string email, string senha)
        {
            // Evitar valores nulos
            if (email == null || senha == null)
            {
                return StatusCode(400, "Senha ou Email não preenchidos!");
            }

            // Criar objeto do usuário a ser encontrado
            Torcedor? user;

            // Tentar
            try
            {
                // Realizar a procura do técnico informado
                user = _torcedorRepository.RealizarLogin(email, senha, true);

                // Caso o usuário seja nulo
                if (user == null)
                {
                    // Retornar que o login de colaborador não foi encontrado
                    return StatusCode(500, "Login de colaborador inválido");
                }

                // Caso tenha encontrado um colaborador
                var token = _tokenRepository.CriarToken(user.id);

                // Retornar o token para que o usueario possa entrar
                return Ok(token);
            }
            catch (Exception e)
            {
                // Caso algum erro ocorra, não 
                return StatusCode(500, $"Não foi possível efetuar o login {e}");
            }
        }
    }

	[ApiController]
	[Route("chamaovar-api/colaborador")]
	public class ColaboradorController : ControllerBase
	{
		// Interface
		private readonly ITorcedorRepository _torcedorRepository;
		private readonly ITokenRepository _tokenRepository;

        // Construtor
        public ColaboradorController(ITorcedorRepository torcedorRepository, ITokenRepository tokenRepository)
        {
            _torcedorRepository = torcedorRepository;
            _tokenRepository = tokenRepository;
        }

        // Requests

    }
}

