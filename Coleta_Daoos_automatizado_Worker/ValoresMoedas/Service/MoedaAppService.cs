using Coleta_Dados_automatizado_Worker.Model;
using Microsoft.EntityFrameworkCore;
using ValoresMoedas_API.Interface;
using ValoresMoedas_API.Model.Dto;
using ValoresMoedas_API.Repository;
using static Coleta_Dados_automatizado_Worker.Model.Dados_Moedas;
using static ValoresMoedas_API.Model.Dto.MoedaDTO;
using AppDbContext = Coleta_Dados_automatizado_Worker.Model.Dados_Moedas.AppDbContext;

namespace ValoresMoedas_API.Service
{
    public class MoedaAppService : IMoedaAppService
    {
        private readonly AppDbContext _context;
        private readonly IMoedaRepository _moedaAppRepository;

        public MoedaAppService(AppDbContext context, IMoedaRepository moedaAppRepository)
        {
            _context = context;
            _moedaAppRepository = moedaAppRepository;
        }

        public async Task<IEnumerable<MoedaDTO>> ObterTodasAsync()
        {
            // 1. Busca as Entidades (Moeda) do banco através do Repositório
            return await _moedaAppRepository.ListarTodosAsync();            
        }
    }
}
