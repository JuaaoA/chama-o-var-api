using chama_o_var_api.Model;

namespace chama_o_var_api.Infra
{
    public class TecnicoRepository : ITecnicoRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Tecnico tecnico)
        {
            _context.Tecnicos.Add(tecnico);
            _context.SaveChanges();
        }

        public List<Tecnico> Get()
        {
            return _context.Tecnicos.ToList();
        }
    }
}
