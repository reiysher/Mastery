# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/Api/Mastery.Api/Mastery.Api.csproj", "src/Api/Mastery.Api/"]
COPY ["src/Modules/Identity/Mastery.Modules.Identity.Infrastructure/Mastery.Modules.Identity.Infrastructure.csproj", "src/Modules/Identity/Mastery.Modules.Identity.Infrastructure/"]
COPY ["src/Common/Mastery.Common.Infrastructure/Mastery.Common.Infrastructure.csproj", "src/Common/Mastery.Common.Infrastructure/"]
COPY ["src/Common/Mastery.Common.Application/Mastery.Common.Application.csproj", "src/Common/Mastery.Common.Application/"]
COPY ["src/Common/Mastery.Common.Domain/Mastery.Common.Domain.csproj", "src/Common/Mastery.Common.Domain/"]
COPY ["src/Modules/Identity/Mastery.Modules.Identity.Application/Mastery.Modules.Identity.Application.csproj", "src/Modules/Identity/Mastery.Modules.Identity.Application/"]
COPY ["src/Modules/Identity/Mastery.Modules.Identity.Domain/Mastery.Modules.Identity.Domain.csproj", "src/Modules/Identity/Mastery.Modules.Identity.Domain/"]
COPY ["src/Modules/Identity/Mastery.Modules.Identity.Presentation/Mastery.Modules.Identity.Presentation.csproj", "src/Modules/Identity/Mastery.Modules.Identity.Presentation/"]
COPY ["src/Common/Mastery.Common.Presentation/Mastery.Common.Presentation.csproj", "src/Common/Mastery.Common.Presentation/"]
RUN dotnet restore "./src/Api/Mastery.Api/Mastery.Api.csproj"
COPY . .
WORKDIR "/src/src/Api/Mastery.Api"
RUN dotnet build "./Mastery.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mastery.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mastery.Api.dll"]