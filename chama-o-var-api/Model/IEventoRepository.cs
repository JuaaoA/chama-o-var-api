using System;
namespace chama_o_var_api.Model
{
    /*
	 * Essa interface dispõe as ações possíveis num repositório dos EVENTOS
	 * Assim, os dados podem ser acessados pelo controlador sem a necessidade
	 * de instanciar um contexto de conexão
	 */
    public interface IEventoRepository
    {
        void Add(Evento evento);

        Evento? GetEventoById(int id);

        List<Evento>? GetByCriadorID(int id, bool inverter);

        bool Update(int id, string nome, DateTime data,
            string detalhes, int minimo_pontuacao);

        List<Evento> Get();
    }
}

