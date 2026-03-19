using Microsoft.EntityFrameworkCore;



namespace ValoresMoedas_API.Model.Dto
{
    public class MoedaDTO
    {
        private DateTime dataColeta;

        public MoedaDTO(string titulo, string valor, string variacao, DateTime data)
        {
            Titulo = titulo;
            Valor = valor;
            Variacao = variacao;
            Data = data;
        }

        public string Titulo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string Variacao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}
