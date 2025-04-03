FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["NutriFit.BackendForFrontend/NutriFit.BackendForFrontend.csproj", "NutriFit.BackendForFrontend/"]
RUN dotnet restore "./NutriFit.BackendForFrontend/NutriFit.BackendForFrontend.csproj"
COPY . .
WORKDIR "/src/NutriFit.BackendForFrontend"
RUN dotnet build "./NutriFit.BackendForFrontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NutriFit.BackendForFrontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NutriFit.BackendForFrontend.dll"]