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
	[Route("/chamaovar-api/evento/ByID")]
	public class EventoByIdController : ControllerBase
	{
        // Interface
        private readonly IEventoRepository _eventoRepository;
        private readonly ITokenRepository _tokenRepository;

        // Construtor
        public EventoByIdController(IEventoRepository eventoRepository, ITokenRepository tokenRepository)
        {
            this._eventoRepository = eventoRepository;
            this._tokenRepository = tokenRepository;
        }

		// Requests
		[HttpGet]
		public IActionResult GetById(int id)
		{
			// Pegar o evento com o ID
			Evento? evento_buscado = _eventoRepository.GetEventoById(id);

			// Se o evento tiver vazio
			if (evento_buscado == null) return StatusCode(500, "Não foi possível encontrar o evento!");

			// Se não, retornar o evento
			return Ok(evento_buscado);
		}
    }

	[ApiController]
	[Route("/chamaovar-api/evento/ByCriador/inverter")]
	public class EventoCriadorInverterController : ControllerBase
	{
        // Interface
        private readonly IEventoRepository _eventoRepository;
        private readonly ITokenRepository _tokenRepository;

        // Construtor
        public EventoCriadorInverterController(IEventoRepository eventoRepository, ITokenRepository tokenRepository)
        {
            this._eventoRepository = eventoRepository;
            this._tokenRepository = tokenRepository;
        }

        // Requests
        [HttpGet]
        public IActionResult GetByCriadorToken(string criadorToken)
        {
            // Verificação de token
            Torcedor? tecnico_criador = _tokenRepository.GetTecnicoByToken(criadorToken);

            if (tecnico_criador == null)
            {
                return StatusCode(500, "Token de colaborador não identificado!");
            }

            // Pegar uma lista em que o criador
            try
            {
                List<Evento>? lista = _eventoRepository.GetByCriadorID(tecnico_criador.id, true);

                return Ok(lista);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao trazer os eventos!");
            }
        }
    }

	[ApiController]
	[Route("/chamaovar-api/evento/ByCriador")]
	public class EventoCriadorController : ControllerBase
	{
        // Interface
        private readonly IEventoRepository _eventoRepository;
        private readonly ITokenRepository _tokenRepository;

        // Construtor
        public EventoCriadorController(IEventoRepository eventoRepository, ITokenRepository tokenRepository)
        {
            this._eventoRepository = eventoRepository;
            this._tokenRepository = tokenRepository;
        }

		// Requests
		[HttpGet]
		public IActionResult GetByCriadorToken(string criadorToken)
		{
			// Verificação de token
			Torcedor? tecnico_criador = _tokenRepository.GetTecnicoByToken(criadorToken);

			if (tecnico_criador == null)
			{
				return StatusCode(500, "Token de colaborador não identificado!");
			}

			// Pegar uma lista em que o criador
			try
			{
                List<Evento>? lista = _eventoRepository.GetByCriadorID(tecnico_criador.id, false);

				return Ok(lista);
            }
			catch
			{
				return StatusCode(500, "Ocorreu um erro ao trazer os eventos!");
			}
		}
    }

	[ApiController]
	[Route("/chamaovar-api/evento")]
	public class EventoController : ControllerBase
	{
		// Interface
		private readonly IEventoRepository _eventoRepository;
		private readonly ITokenRepository _tokenRepository;

		// Construtor
		public EventoController(IEventoRepository eventoRepository, ITokenRepository tokenRepository)
		{
			this._eventoRepository = eventoRepository;
			this._tokenRepository = tokenRepository;
		}

		// Requests
		[HttpPost]
		public IActionResult Add(string nome, DateTime data, string detalhes, int? minimo_pontuacao, string token)
		{
			// Verificação de token
			Torcedor? tecnico_criador = _tokenRepository.GetTecnicoByToken(token);

            if (tecnico_criador == null)
			{
				return StatusCode(500, "Token de colaborador não identificado!");
			}

			// Evitar Erros - dados nulos ou incorretos
			if (nome == "" || nome == null)
			{
				return StatusCode(500, "Por favor digite o nome do evento!");
			}

			if (detalhes == "" || detalhes == null)
			{
				return StatusCode(500, "Por favor digite os detalhes do evento");
			}

			if (minimo_pontuacao > 1000 || minimo_pontuacao < 0)
			{
				return StatusCode(500, "Pontuação mínima inválida!");
			}

			if (minimo_pontuacao == null)
			{
				minimo_pontuacao = 0;
			}

			// Criar o Evento
			Evento evento = new Evento(nome, data, detalhes, (int)minimo_pontuacao, tecnico_criador.id);

			// Adicionar ao banco de dados e salvar
			try
			{
				// Adicionar Evento
                _eventoRepository.Add(evento);

                // Retornar que deu certo
                return Ok();
            }
			catch (Exception e)
			{
				// Retornar o erro
				return StatusCode(500, $"Server-side - Ocorreu um erro: {e}");
			}
		}

		[HttpPatch]
		public IActionResult Edit(int id, string nome, DateTime data,
			string detalhes, int? minimo_pontuacao , string token)
		{
            // Verificação de token
            Torcedor? tecnico_editor = _tokenRepository.GetTecnicoByToken(token);

            if (tecnico_editor == null) return StatusCode(500, "Token de colaborador não identificado!");

			// Pegar o evento a ser editado
			Evento? evento_editar = _eventoRepository.GetEventoById(id);

			// Se o evento for nulo
			if (evento_editar == null) return StatusCode(500, "O evento com esse ID não foi encontrado.");

			// O editor precisa ser o mesmo que o criador
			if (evento_editar.criador != tecnico_editor.id) return StatusCode(500, "O editor não é o mesmo que o criador!");

            // Evitar erros - Dados nulos
            if (nome == "" || nome == null) return StatusCode(500, "Por favor digite o nome do evento!");
            
            if (detalhes == "" || detalhes == null) return StatusCode(500, "Por favor digite os detalhes do evento");

            if (minimo_pontuacao > 1000 || minimo_pontuacao < 0) return StatusCode(500, "Pontuação mínima inválida!");

            if (minimo_pontuacao == null) minimo_pontuacao = 0;

			// Por fim, editar o evento
			if (!_eventoRepository.Update(id, nome, data, detalhes, (int)minimo_pontuacao))
			{
				return StatusCode(500, "Ocorreu um erro ao editar evento.");
			}

			// Caso deu certo
			return Ok();
        }

		[HttpGet]
		public IActionResult Get()
		{
			// Retornar a lista dos eventos
			return Ok(_eventoRepository.Get());
		}
	}
}

