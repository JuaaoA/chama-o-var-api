using chama_o_var_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace chama_o_var_api.Controllers
{
    [ApiController]
    [Route("chamaovar-api/tecnico")]
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoRepository _tecnicoRepository;

        public TecnicoController(ITecnicoRepository tecnicoRepository)
        {
            _tecnicoRepository = tecnicoRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IActionResult Add(string nome, string cpf)
        {
            var novoTecnico = new Tecnico(nome, cpf);
            _tecnicoRepository.Add(novoTecnico);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tecnicos = _tecnicoRepository.Get();

            return Ok(tecnicos);
        }
    }
}
