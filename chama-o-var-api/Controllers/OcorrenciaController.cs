using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using chama_o_var_api.Model;
using chama_o_var_api.Infra;

namespace chama_o_var_api.Controllers
{
    // ROTAS
    [ApiController]
    [Route("chamaovar-api/ocorrencia/procurar/")]
    public class OcorrenciaGeneralSearchController : ControllerBase
    {
        // Interface
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly ITorcedorRepository _torcedorRepository;

        // Construtor
        public OcorrenciaGeneralSearchController(IOcorrenciaRepository
            ocorrenciaRepository, ITorcedorRepository torcedorRepository)
        {
            _ocorrenciaRepository = ocorrenciaRepository;
            _torcedorRepository = torcedorRepository;
        }

        // Requests
        // PESQUISAR OCORRENCIAS
        [HttpGet]
        public IActionResult PesquisaOcorrencia(int torcedorId)
        {
            return Ok();
        }
    }

    [ApiController]
	[Route("chamaovar-api/ocorrencia/procurar/torcedor")]
	public class OcorrenciaSearchByTorcedorController : ControllerBase
	{
		// Interface
		private readonly IOcorrenciaRepository _ocorrenciaRepository;
		private readonly ITorcedorRepository _torcedorRepository;

        // Construtor
        public OcorrenciaSearchByTorcedorController(IOcorrenciaRepository
			ocorrenciaRepository, ITorcedorRepository torcedorRepository)
        {
            _ocorrenciaRepository = ocorrenciaRepository;
            _torcedorRepository = torcedorRepository;
        }

		// Requests
		// PEGAR OCORRENCIAS POR ID DO TORCEDOR
		[HttpGet]
		public IActionResult PegarPorIDTorcedor(int torcedorId)
		{
            // Verificar se primeiro o torcedor existe
            if (!_torcedorRepository.UsuarioJaExiste(torcedorId))
            {
                // Retornar um erro
                return StatusCode(500, "Torcedor não encontrado!");
            }

            // Tentar
            List<Ocorrencia>? ocorren;
            try
			{
				// Pegar todas as ocorrências usando o torcedorID
				ocorren = _ocorrenciaRepository.GetAllByTorcedorId(torcedorId);
			}
			catch
			{
				// Retornar erro
				return StatusCode(500, "Não foi possível trazer as ocorrências");
			}

			// Retornar que tudo deu certo + a lista de ocorrências
			return Ok(ocorren);
		}
    }

	[ApiController]
	[Route("chamaovar-api/ocorrencia/")]
	public class OcorrenciaController : ControllerBase
	{
		// Interface
		private readonly IOcorrenciaRepository _ocorrenciaRepository;
		private readonly ITorcedorRepository _torcedorRepository;

		// Construtor
		public OcorrenciaController(IOcorrenciaRepository ocorrenciaRepository, ITorcedorRepository torcedorRepository)
		{
			_ocorrenciaRepository = ocorrenciaRepository;
			_torcedorRepository = torcedorRepository;
		}

		// Requests

		// ADICIONAR OCORRENCIA
		[HttpPost]
		public IActionResult Add(string acontecimento, DateTime data, int penalidade,
			int torcedor, int colaborador)
		{
			// EVITAR ERROS - DADOS NULOS
			if (acontecimento == "" || penalidade <= 0)
			{
				// Retornar um erro
				return StatusCode(500, "Um ou mais dados estão inválidos!");
			}

			// 2 - ERROS DE UNICIDADE -> É necessário que o colaborador e torcedor existam
			if (!(_torcedorRepository.UsuarioJaExiste(torcedor) && _torcedorRepository.UsuarioJaExiste(colaborador)))
			{
				// Retornar um erro
				return StatusCode(500, "Torcedor ou colaborador não encontrado!");
			}

			// 3 - TORCEDOR E COLABORADOR IGUAIS
			if (torcedor == colaborador)
			{
				return StatusCode(500, "Colaborador é o mesmo do torcedor");
			}

			// Criar ocorrencia e confirmar no BD
			var novaOcorrencia = new Ocorrencia(acontecimento, data, penalidade,
				torcedor, colaborador);

			_ocorrenciaRepository.Add(novaOcorrencia);

			// Retornar código 200
			return Ok();
		}

		// EDITAR OCORRENCIA
		[HttpPatch]
		public IActionResult PatchOcorrencia(int id, string acontecimento, DateTime data, int penalidade)
		{
			// Procurar a ocorrência a ser editada
			Ocorrencia? oco = _ocorrenciaRepository.GetOneById(id);

			// Se ocorrencia for nula
			if (oco == null)
			{
				return StatusCode(500, "Não foi encontrada a ocorrência com ID");
			}

			// Se encontrou a ocorrência, editar o que pode ser mudado
			bool resultado = _ocorrenciaRepository.Update(id, acontecimento, data, penalidade);

			// Se caso os resultados não tenham dado certo
			if (!resultado)
			{
				// Retornar erro
				return StatusCode(500, "Ocorreu um erro ao salvar a alteração");
			}

			// Retornar que tudo deu certo junto com a mensagem já editada
			return Ok(oco);
		}

		// PEGAR TODAS OCORRENCIAS
		[HttpGet]
		public IActionResult Get()
		{
			// Tentar
			try
			{
				// Pegar todas ocorrencias disponiveis
                var ocorrencias = _ocorrenciaRepository.Get();

				// Retornar ocorrências
                return Ok(ocorrencias);
            }
			catch (Exception E)
			{
				// Retornar o erro com detalhe
				return StatusCode(500, $"Não foi possível trazer as ocorrências: {E}");
			}
		}
	}
}

