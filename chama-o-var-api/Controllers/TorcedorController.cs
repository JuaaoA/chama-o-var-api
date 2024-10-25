using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using chama_o_var_api.Model;
using chama_o_var_api.Infra;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace chama_o_var_api.Controllers
{
    // ROTAS OUTRO LUGAR
    [ApiController]
    [Route("chamaovar-api/torcedor/login")]
    public class TorcedorLogin : ControllerBase
    {
        // Interface
        private readonly ITorcedorRepository _torcedorRepository;

        // Construtor
        public TorcedorLogin(ITorcedorRepository torcedorRepository)
        {
            _torcedorRepository = torcedorRepository;
        }

        // Request
        [HttpGet]
        public IActionResult TentarLogin(string email, string senha)
        {

            // Se não todo terminar
            return StatusCode(500, $"Login Inválido");
        }
    }

    // Verificação de unicidade
    [ApiController]
    [Route("chamaovar-api/torcedor/verificar-unico")]
    public class TorcedorUnicidadeController : ControllerBase
    {
        // Interface
        private readonly ITorcedorRepository _torcedorRepository;

        // Construtor
        public TorcedorUnicidadeController(ITorcedorRepository torcedorRepository)
        {
            _torcedorRepository = torcedorRepository;
        }

        // Request
        [HttpGet]
        public IActionResult VerificarUnico(string cpf, string email, string telefone)
        {
            string resultado = _torcedorRepository.PossuiCredenciaisUnicas(cpf, email, telefone);

            // Verificar se as credencias são unicas ou ja existem
            if (resultado.Length == 0)
            {
                return Ok();
            }

            // Se não
            return StatusCode(500, $"O Campo {resultado} já existe no sistema!");
        }
    }

    [ApiController]
    [Route("chamaovar-api/torcedor")]
    public class TorcedorController : ControllerBase
    {
        // Interface
        private readonly ITorcedorRepository _torcedorRepository;

        // Construtor
        public TorcedorController(ITorcedorRepository torcedorRepository)
        {
            _torcedorRepository = torcedorRepository;
        }

        // Requests
        [HttpPost]
        public IActionResult Add(string nome_completo, string cpf, string email,
            string telefone, DateTime nascimento, string senha, bool tecnico)
        {
            Console.WriteLine(senha);

            // TRIM nas strings
            cpf = cpf.Trim();
            nome_completo = nome_completo.Trim();
            email = email.Trim();
            telefone = telefone.Trim();

            /*
             * Verificações antes do envio
             */

            // Email - Caso não esteja válido
            if (!InputValidation.ValidarEmail(email))
            {
                // Retornar erro
                return StatusCode(500, new {message = "O Email está inválido!"});
            }
            // CPF - caso não esteja válido
            if (!InputValidation.ValidarCPF(cpf))
            {
                // Retornar erro
                return StatusCode(500, new
                {
                    message = "O CPF está inválido! " +
                    "Certifique que seja apenas numeros e que o número de " +
                    "caracteres está correto."
                });
            }
            // Nascimento - caso não esteja válido
            if (!InputValidation.ValidarDataNasc(nascimento))
            {
                return StatusCode(500, new
                {
                    message = "A Data de Nascimento está invalida!"
                });
            }
            // Telefone - caso não esteja válido
            if (!InputValidation.ValidarTelefone(telefone))
            {
                return StatusCode(500, new
                {
                    message = "O telefone está inválido!"
                });
            }
           


            // Criar o torcedor novo e confimar
            var novoTorcedor = new Torcedor(nome_completo, cpf, email, telefone, nascimento, senha, tecnico);
            _torcedorRepository.Add(novoTorcedor);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var torcedores = _torcedorRepository.Get();

            return Ok(torcedores);
        }
    }
}

