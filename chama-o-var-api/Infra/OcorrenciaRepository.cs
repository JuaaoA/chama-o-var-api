using System;
using chama_o_var_api.Model;

namespace chama_o_var_api.Infra
{
	public class OcorrenciaRepository : IOcorrenciaRepository 
	{
		private readonly ConnectionContext _context = new ConnectionContext();

        // Funções / Queria colocar meto-dos, mas o VS acha que é TO-DO kkk odio
        public void Add(Ocorrencia ocorrencia)
        {
            _context.Ocorrencias.Add(ocorrencia);
            _context.SaveChanges();
        }

        public List<Ocorrencia> Get()
        {
            return _context.Ocorrencias.ToList();
        }

        public List<Ocorrencia>? GetAllByTorcedorId(int torcedorId)
        {
            // Tentar
            try
            {
                // Pegar todas as ocorrências com o id de um usuário e transformar em lista
                return _context.Ocorrencias.Where(oco => oco.torcedor == torcedorId).ToList();
            }
            catch
            {
                // Retonar nulo
                return null;
            }
        }

        public Ocorrencia? GetOneById(int id)
        {
            // Tentar
            try
            {
                // Procurar ocorrência pelo ID da própria ocorrência
                return _context.Ocorrencias.FirstOrDefault(ocorrencia => ocorrencia.id == id);
            }
            catch (Exception e)
            {
                // Retornar nulo caso erro
                return null;
            }
        }

        public bool Update(int id, string acontecimento, DateTime data, int penalidade)
        {
            // Pegar a ocorrência
            Ocorrencia? oco = null;
            try
            {
                oco = _context.Ocorrencias.FirstOrDefault(ocorrencia => ocorrencia.id == id);
            }
            catch
            {
                // Retornar erro
                return false;
            }

            // Caso seja nulo
            if (oco == null)
            {
                // Retornar erro
                return false;
            }

            // Tentar
            try
            {
                // Adicionar mudanças
                oco.acontecimento = acontecimento;
                oco.data_ocorrencia = data;
                oco.penalidade = penalidade;

                // Salvar tudo
                _context.SaveChanges();

                // Retornar que tudo deu certo
                return true;
            }
            catch
            {
                // Retornar erro
                return false;
            }
        }
    }
}

