using Coleta_Dados_automatizado_Worker.Model;
using ValoresMoedas_API.Model.Dto;


namespace ValoresMoedas_API.Interface
{
    public interface IMoedaRepository
    {
        Task<IEnumerable<MoedaDTO>> ListarTodosAsync();

    }
}
