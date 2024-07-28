FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Mastery.Career.Api/Mastery.Career.Api.csproj", "src/Mastery.Career.Api/"]
COPY ["src/Mastery.Career.Application/Mastery.Career.Application.csproj", "src/Mastery.Career.Application/"]
COPY ["src/Mastery.Career.Domain/Mastery.Career.Domain.csproj", "src/Mastery.Career.Domain/"]
COPY ["src/Mastery.Career.Infrastructure/Mastery.Career.Infrastructure.csproj", "src/Mastery.Career.Infrastructure/"]
RUN dotnet restore "./src/Mastery.Career.Api/Mastery.Career.Api.csproj"
COPY . .
WORKDIR "/src/src/Mastery.Career.Api"
RUN dotnet build "./Mastery.Career.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mastery.Career.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mastery.Career.Api.dll"]