using System;
using chama_o_var_api.Model;

namespace chama_o_var_api.Infra
{
	public class EventoRepository : IEventoRepository
	{
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        public bool Update(int id, string nome, DateTime data,
            string detalhes, int minimo_pontuacao)
        {

            // Criar evento
            Evento? evt = null;

            try
            {
                // Procurar evento com esse id
                evt = _context.Eventos.FirstOrDefault(eve => eve.id == id);
            }
            catch
            {
                return false;
            }

            // Se for nulo
            if (evt == null)
            {
                return false;
            }

            // Tentar editar o evento
            try
            {
                // Editar
                evt.nome = nome;
                evt.data_evento = data;
                evt.detalhes = detalhes;
                evt.minimo_pontuacao = minimo_pontuacao;

                // Salvar mudanças
                _context.SaveChanges();

                // Retornar sucesso
                return true;
            }
            catch
            {
                // Caso dê erro
                return false;
            }
        }

        public List<Evento>? GetByCriadorID(int id, bool inverter)
        {
            try
            {
                // Pegar os eventos com o id do criador
                List<Evento>? eventos;

                if (!inverter)
                {
                    eventos = _context.Eventos.Where(evt => evt.criador == id).ToList();
                }
                else
                {
                    eventos = _context.Eventos.Where(evt => evt.criador != id).ToList();
                }
                
                //
                return eventos;
            }
            catch
            {
                return null;
            }
        }

        public Evento? GetEventoById(int id)
        {
            try
            {
                // Pegar o primeiro(e unico) evento que estiver com esse id
                Evento? evento = _context.Eventos.SingleOrDefault(evt => evt.id == id);

                return evento;
            }
            catch
            {
                return null;
            }

            
        }

        public List<Evento> Get()
        {
            return _context.Eventos.ToList();
        }
    }
}

