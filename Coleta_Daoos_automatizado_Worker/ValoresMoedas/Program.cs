using Microsoft.EntityFrameworkCore;
using ValoresMoedas_API.Interface;
using ValoresMoedas_API.Repository;
using ValoresMoedas_API.Service;
using static Coleta_Dados_automatizado_Worker.Model.Dados_Moedas;


//--------------------------

var builder = WebApplication.CreateBuilder(args);

// 1. REGISTRO DOS SERVIÇOS BASE DA API (O QUE ESTÁ FALTANDO)
builder.Services.AddControllers(); // <-- ESTA LINHA RESOLVE O SEU ERRO ATUAL
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(); // Resolva o erro anterior também

// 2. SEUS SERVIÇOS (DDD / INJEÇÃO DE DEPENDÊNCIA)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(@"C:\Projetos\DadosMoedas.db"));

builder.Services.AddScoped<IMoedaAppService, MoedaAppService>();
builder.Services.AddScoped<IMoedaRepository, MoedaRepository>();


var app = builder.Build();

// 3. CONFIGURAÇÃO DO PIPELINE (A ORDEM IMPORTA!)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// MAPEAMENTO DAS ROTAS
app.MapControllers(); // Sem o AddControllers() lá em cima, essa linha falha.

app.Run();