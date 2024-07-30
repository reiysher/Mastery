FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/Mastery.Api/Mastery.Api.csproj", "src/API/Mastery.Api/"]
RUN dotnet restore "./src/API/Mastery.Api/Mastery.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Mastery.Api"
RUN dotnet build "./Mastery.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mastery.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mastery.Api.dll"]