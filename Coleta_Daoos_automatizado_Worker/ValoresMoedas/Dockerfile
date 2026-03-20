FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Remove TODOS os arquivos appsettings da pasta do Worker para evitar o erro NETSDK1152
# Usamos o caminho relativo que o log mostrou: Coleta_Daoos_automatizado_Worker/
RUN find Coleta_Daoos_automatizado_Worker -name "appsettings*.json" -delete

RUN dotnet restore "ValoresMoedas/ValoresMoedas_API.csproj"

FROM build AS publish-stage
# Adicionamos uma flag extra /p:ErrorOnDuplicatePublishOutputFiles=false para garantir
RUN dotnet publish "ValoresMoedas/ValoresMoedas_API.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:ErrorOnDuplicatePublishOutputFiles=false

FROM base AS final
WORKDIR /app
COPY --from=publish-stage /app/publish .
RUN mkdir -p /app/data
ENTRYPOINT ["dotnet", "ValoresMoedas_API.dll"]