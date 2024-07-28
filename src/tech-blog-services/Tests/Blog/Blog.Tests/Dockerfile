#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tests/Blog/Blog.Tests/Blog.IntegrationTests.csproj", "Tests/Blog/Blog.Tests/"]
COPY ["Services/Blog/Blog.API/Blog.API.csproj", "Services/Blog/Blog.API/"]
COPY ["Services/Blog/Blog.Application/Blog.Application.csproj", "Services/Blog/Blog.Application/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
COPY ["Services/Blog/Blog.Domain/Blog.Domain.csproj", "Services/Blog/Blog.Domain/"]
COPY ["Services/Blog/Blog.Infrastructure/Blog.Infrastructure.csproj", "Services/Blog/Blog.Infrastructure/"]
RUN dotnet restore "./Tests/Blog/Blog.Tests/Blog.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/Tests/Blog/Blog.Tests"
RUN dotnet build "./Blog.IntegrationTests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blog.IntegrationTests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.IntegrationTests.dll"]