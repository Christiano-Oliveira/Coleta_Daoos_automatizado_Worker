FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Remove o que não é necessário para o Worker
RUN rm -rf ValoresMoedas

RUN dotnet restore "Coleta_Daoos_automatizado_Worker/Coleta_Dados_automatizado_Worker.csproj"

FROM build AS publish-stage
RUN dotnet publish "Coleta_Daoos_automatizado_Worker/Coleta_Dados_automatizado_Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish-stage /app/publish .
RUN mkdir -p /app/data
ENTRYPOINT ["dotnet", "Coleta_Dados_automatizado_Worker.dll"]