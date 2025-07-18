# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7090
EXPOSE 5284


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DesafioAEVO.API/DesafioAEVO.API.csproj", "src/DesafioAEVO.API/"]
COPY ["src/DesafioAEVO.Application/DesafioAEVO.Application.csproj", "src/DesafioAEVO.Application/"]
COPY ["src/DesafioAEVO.Domain/DesafioAEVO.Domain.csproj", "src/DesafioAEVO.Domain/"]
COPY ["src/shared/DesafioAEVO.Communication/DesafioAEVO.Communication.csproj", "src/shared/DesafioAEVO.Communication/"]
COPY ["src/shared/DesafioAEVO.Exceptions/DesafioAEVO.Exceptions.csproj", "src/shared/DesafioAEVO.Exceptions/"]
COPY ["src/DesafioAEVO.Infrastructure/DesafioAEVO.Infrastructure.csproj", "src/DesafioAEVO.Infrastructure/"]
RUN dotnet restore "./src/DesafioAEVO.API/DesafioAEVO.API.csproj"
COPY . .
WORKDIR "/src/src/DesafioAEVO.API"
RUN dotnet build "./DesafioAEVO.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DesafioAEVO.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioAEVO.API.dll"]