namespace chama_o_var_api.Model
{
    public interface ITecnicoRepository
    {
        void Add(Tecnico tecnico);

        List<Tecnico> Get();
    }
}
