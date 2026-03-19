using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using static Coleta_Dados_automatizado_Worker.Model.Dados_Moedas;

namespace Coleta_Dados_automatizado_Worker
{

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string _url = "https://economia.uol.com.br/cotacoes/";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Iniciando scraping: {time}", DateTimeOffset.Now);

                try
                {
                    await ExtrairESalvarDados();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Erro no scraping: {message}", ex.Message);
                }

                // Espera 1 hora antes de rodar novamente (exemplo)
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task ExtrairESalvarDados()
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(_url);

            // 1. Seleciona todas as linhas de dados da tabela
            var linhas = doc.DocumentNode.SelectNodes("//tr[@class='linhaDados']");

            if (linhas != null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlite("Data Source=DadosMoedas.db");

                using (var db = new AppDbContext(optionsBuilder.Options))
                {
                    db.Database.EnsureCreated(); // Cria o banco se não existir
                    foreach (var linha in linhas)
                    {
                        // O título está na primeira célula (td) com a classe "title"
                        var tituloNode = linha.SelectSingleNode("./td[@class='title']/a");
                        if (tituloNode == null)
                            continue;

                        // Usamos HtmlEntity.DeEntitize para remover espaços "fantasmas" (&nbsp;)
                        string titulo = tituloNode != null ? HtmlEntity.DeEntitize(tituloNode?.InnerText).Trim() : "";

                        // A variação está na segunda célula (pode ter classe 'up' ou 'down')
                        var variacaoNode = linha.SelectSingleNode("./td[2]");
                        var variacao = variacaoNode != null ? HtmlEntity.DeEntitize(variacaoNode?.InnerText).Trim() : "";
                        // O valor (R$) está na terceira célula
                        var valorNode = linha.SelectSingleNode("./td[3]");
                        var valor = valorNode != null ? HtmlEntity.DeEntitize(valorNode?.InnerText).Trim() : "";

                        db.DadosMoedas.Add(new Dados
                        {
                            Titulo = titulo,
                            valor = valor,
                            variacao = variacao,
                            DataColeta = DateTime.Now
                        });
                        Console.WriteLine($"Moeda: {titulo.PadRight(15)} | Variação: {variacao.PadRight(8)} | Valor: {valor}");
                    }
                    await db.SaveChangesAsync();
                }               
                
            }
            else
            {
                Console.WriteLine("Dados não encontrados.");
            }

        }
    }
}