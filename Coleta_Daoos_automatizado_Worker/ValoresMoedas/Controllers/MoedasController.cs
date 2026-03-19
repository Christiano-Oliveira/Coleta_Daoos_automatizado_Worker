using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValoresMoedas_API.Interface;
using ValoresMoedas_API.Model.Dto;
using static Coleta_Dados_automatizado_Worker.Model.Dados_Moedas;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ValoresMoedas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoedasController : ControllerBase
    {
        private readonly IMoedaAppService _moedaAppService;

        // Injeção de Dependência via Construtor
        public MoedasController(IMoedaAppService moedaAppService)
        {
            _moedaAppService = moedaAppService;
        }

        //GET: api/<MoedasControllerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoedaDTO>>> GetDadosMoedas()
        {
            var valores = await _moedaAppService.ObterTodasAsync();
            return Ok(valores);
        }
    }
}
