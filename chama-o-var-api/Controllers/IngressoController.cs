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
	[Route("/chamaovar-api/ingresso/byallIDs")]
	public class IngressoByAllIdsController : ControllerBase
	{
        // Interfaces
        private readonly IIngressoRepository _ingressoRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IEventoRepository _eventoRepository;

        // Construtor
        public IngressoByAllIdsController(IIngressoRepository ingressoRepository,
            ITokenRepository tokenRepository, IEventoRepository eventoRepository)
        {
            _ingressoRepository = ingressoRepository;
            _tokenRepository = tokenRepository;
            _eventoRepository = eventoRepository;
        }

		// Requests
		[HttpGet]
		public IActionResult GetByAllIDs(int idEvento, string tokenUsuario)
		{
			try
			{
				// Pegar o torcedor
				Torcedor? tcdr = _tokenRepository.GetTorcedorByToken(tokenUsuario);

				// Se torcedor não existir
				if (tcdr == null) return StatusCode(500, "Não foi possível encontrar o torcedor!");

				// Pegar o id do evento e o torcedor e procurar ingresso
				Ingresso? ing = _ingressoRepository.GetByIDS(tcdr.id, idEvento);

				if (ing == null) return StatusCode(400, "Não existe ingresso");

				// Por fim, retornar o ingresso
				return Ok(ing);
			}
			catch
			{
				return StatusCode(500, "Não foi possivel procurar ingresso!");
			}
		}
    }

	[ApiController]
	[Route("/chamaovar-api/ingresso/ByEvento")]
	public class IngressoByEventoController : ControllerBase
	{
        // Interfaces
        private readonly IIngressoRepository _ingressoRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IEventoRepository _eventoRepository;

        // Construtor
        public IngressoByEventoController(IIngressoRepository ingressoRepository,
            ITokenRepository tokenRepository, IEventoRepository eventoRepository)
        {
            _ingressoRepository = ingressoRepository;
            _tokenRepository = tokenRepository;
            _eventoRepository = eventoRepository;
        }

		// Requests
		[HttpGet]
		public IActionResult GetByEvento(int eventoId)
		{
			// Pegar lista de ingressos com esse evento
			try
			{
                List<Ingresso>? ingressos = _ingressoRepository.GetByEvento(eventoId);

				// Retornar
				return Ok(ingressos);
            }
			catch
			{
				return StatusCode(500, "Não foi possível trazer ingressos!");
			}
		}
    }

	[ApiController]
	[Route("/chamaovar-api/ingresso")]
	public class IngressoController : ControllerBase
	{
		// Interfaces
		private readonly IIngressoRepository _ingressoRepository;
		private readonly ITokenRepository _tokenRepository;
		private readonly IEventoRepository _eventoRepository;

		// Construtor
		public IngressoController(IIngressoRepository ingressoRepository,
			ITokenRepository tokenRepository, IEventoRepository eventoRepository)
		{
			_ingressoRepository = ingressoRepository;
			_tokenRepository = tokenRepository;
			_eventoRepository = eventoRepository;
		}

		// Requests
		[HttpPost]
		public IActionResult Add(string tokenTorcedor, int eventoId)
		{
			// Verificar se o token de torcedor é válido
			Torcedor? tcdr = _tokenRepository.GetTorcedorByToken(tokenTorcedor);

			// Se for nulo
			if (tcdr == null) return StatusCode(500, "Torcedor não foi encontrado ou token inválido!");

			// Depois, pegar os dados do evento
			Evento? evnt = _eventoRepository.GetEventoById(eventoId);

			// Se for nulo
			if (evnt == null) return StatusCode(500, "Evento não foi encontrado!");

			// Verificar erros

			// Evitar que o criador possa ter um ingresso no próprio evento
			if (evnt.criador == tcdr.id) return StatusCode(500, "O criador do evento não pode ter ingresso!");

			// Evitar que o torcedor tenha um ingresso estando com pontuação baixa
			if (tcdr.score < evnt.minimo_pontuacao) return StatusCode(500, "Pontos insuficientes!");

			// Verificar se o torcedor já possui um ingresso nesse evento
			if (_ingressoRepository.GetByIDS(tcdr.id, evnt.id) != null)
			{
				return StatusCode(500, "Esse usuário já possui um ingresso nesse evento!");
			}

			// Finalmente, criar o ingresso
			Ingresso ingresso = new Ingresso(tcdr.id, evnt.id);

			// E adicionar na lista de ingressos
			try
			{
				// Enviar o ingresso
				_ingressoRepository.Add(ingresso);

				// Indicar que deu certo
				return Ok();
			}
			catch (Exception e)
			{
				// Retornar o erro
				return StatusCode(500, $"Ocorreu um erro ao adicionar ingresso: {e}");
			}
		}

		[HttpDelete]
		public IActionResult Delete(int id, string torcedorToken)
		{
			// Verificação de torcedor
			Torcedor? tcdr = _tokenRepository.GetTorcedorByToken(torcedorToken);

			if (tcdr == null) return StatusCode(500, "Não foi encontrado torcedor com esse token!");

			Ingresso? ing = _ingressoRepository.GetByID(id);

			if (ing == null) return StatusCode(500, "Não foi possível encontrar ingresso com esse ID!");

			// Verificar se o torcedor pode excluir dado
			if (tcdr.id != ing.torcedor) return StatusCode(500, "Somente o dono do ingresso pode exclui-lo!");

			// Realizar o delete
			bool resultado = _ingressoRepository.Delete(id);

			// Ser der errado
			if (!resultado)
			{
				return StatusCode(500, "Ocorreu um erro ao apagar ingresso!");
			}

			// Se não
			return Ok();
		}
	}
}

