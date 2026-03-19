using Microsoft.EntityFrameworkCore;


namespace Coleta_Dados_automatizado_Worker.Model
{
    public class Dados_Moedas
    {
        public class Dados
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string valor { get; set; }
            public string variacao { get; set; }
            public DateTime DataColeta { get; set; }
        }

        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
            public DbSet<Dados> DadosMoedas { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                string caminhoBanco = @"C:\Projetos\DadosMoedas.db"; 
                options.UseSqlite("Data Source=" + caminhoBanco);
            }
        }   
    }
}
