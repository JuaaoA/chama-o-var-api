using System;
namespace chama_o_var_api.Model
{
    /*
	 * Essa interface dispõe as ações possíveis num repositório dos EVENTOS
	 * Assim, os dados podem ser acessados pelo controlador sem a necessidade
	 * de instanciar um contexto de conexão
	 */
    public interface IIngressoRepository
    {
        void Add(Ingresso ingresso);

        Ingresso? GetByIDS(int torcedor, int evento);

        List<Ingresso>? GetByEvento(int eventoId);

        Ingresso? GetByID(int id);

        bool Delete(int id);

        List<Ingresso> Get();
    }
}

