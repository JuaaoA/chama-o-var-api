using System;
using System.Linq;
using chama_o_var_api.Model;

namespace chama_o_var_api.Infra
{
	public class IngressoRepository : IIngressoRepository
	{
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Ingresso ingresso)
        {
            _context.Ingressos.Add(ingresso);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            // Tentar
            try
            {
                // Remover o ingresso no BD
                _context.Ingressos.Remove(_context.Ingressos.Single(ing => ing.id == id));

                // Salvar
                _context.SaveChanges();

                // Retornar que deu certo
                return true;
            }
            catch
            {
                // Retornar que não foi deletada
                return false;
            }
        }

        public Ingresso? GetByIDS(int torcedor, int evento)
        {
            // Ingresso
            Ingresso? ingresso;

            try
            {
                // Procurar com esses ids de torcedor e evento
                ingresso = _context.Ingressos.SingleOrDefault(ing => ing.torcedor == torcedor && ing.evento == evento);

                // Retornar o ingresso que foi achado ou nulo caso não achar
                return ingresso;
            }
            catch
            {
                // Caso erro, retorne nulo
                return null;
            }
        }

        public List<Ingresso>? GetByEvento(int eventoId)
        {
            // Ingressos
            List<Ingresso>? ingressos;

            try
            {
                // Procurar com esse id de evento
                ingressos = _context.Ingressos.Where(ing => ing.evento == eventoId).ToList();

                // Retornar a lista de ingressos
                return ingressos;
            }
            catch
            {
                // Casso erro, retorne nulo
                return null;
            }
        }

        public Ingresso? GetByID(int id)
        {
            // Ingresso
            Ingresso? ingresso;

            try
            {
                // Procurar com esse id
                ingresso = _context.Ingressos.SingleOrDefault(ing => ing.id == id);

                // Retornar o ingresso que foi achado ou nulo caso não achar
                return ingresso;
            }
            catch
            {
                // Caso erro, retorne nulo
                return null;
            }
        }

        public List<Ingresso> Get()
        {
            return _context.Ingressos.ToList();
        }
    }
}

