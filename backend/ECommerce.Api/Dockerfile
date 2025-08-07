# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiando os .csproj separadamente
COPY ["ECommerce.Api/ECommerce.Api.csproj", "ECommerce.Api/"]
COPY ["ECommerce.Domain/ECommerce.Domain.csproj", "ECommerce.Domain/"]

# Restaurando as dependências
RUN dotnet restore "ECommerce.Api/ECommerce.Api.csproj"

# Copiando todos os arquivos (inclui .cs, .csproj, etc)
COPY . .

# Publicando a aplicação
RUN dotnet publish "ECommerce.Api/ECommerce.Api.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.dll"]
