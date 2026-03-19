using Microsoft.EntityFrameworkCore;
using ValoresMoedas_API.Interface;
using ValoresMoedas_API.Model.Dto;
using static Coleta_Dados_automatizado_Worker.Model.Dados_Moedas;

namespace ValoresMoedas_API.Repository
{
    public class MoedaRepository : IMoedaRepository
    {
        private readonly AppDbContext _context;

        public MoedaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MoedaDTO>> ListarTodosAsync()
        {
            var dados = await _context.DadosMoedas
                .OrderByDescending(x => x.DataColeta)
                .ToListAsync();

            return dados.Select(m => new MoedaDTO(m.Titulo, m.valor, m.variacao, m.DataColeta));
        }

    }
}
