using Coleta_Dados_automatizado_Worker.Model;
using Microsoft.AspNetCore.Mvc;
using ValoresMoedas_API.Model.Dto;


namespace ValoresMoedas_API.Interface
{
    public interface IMoedaAppService
    {
        Task<IEnumerable<MoedaDTO>> ObterTodasAsync();

    }
}
