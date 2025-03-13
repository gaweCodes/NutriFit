FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_22.x | bash
RUN apt-get -y install nodejs
RUN npm install -g @angular/cli

FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NutriFit.Web.Angular.BackendForFrontend/NutriFit.Web.Angular.BackendForFrontend.csproj", "NutriFit.Web.Angular.BackendForFrontend/"]
COPY ["NutriFit.Web.Angular/NutriFit.Web.Angular.esproj", "NutriFit.Web.Angular/"]
RUN dotnet restore "./NutriFit.Web.Angular.BackendForFrontend/NutriFit.Web.Angular.BackendForFrontend.csproj"
COPY . .
WORKDIR "/src/NutriFit.Web.Angular.BackendForFrontend"
RUN dotnet build "./NutriFit.Web.Angular.BackendForFrontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NutriFit.Web.Angular.BackendForFrontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NutriFit.Web.Angular.BackendForFrontend.dll"]
