FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# restore
WORKDIR /src

COPY src/Shamyr.Urlik.Service/Shamyr.Urlik.Service.csproj Shamyr.Urlik.Service/
COPY src/Shamyr.Urlik.Database/Shamyr.Urlik.Database.csproj Shamyr.Urlik.Database/

COPY NuGet.config .

WORKDIR Shamyr.Urlik.Service
RUN dotnet restore --configfile /src/NuGet.config

# build
WORKDIR /src

COPY src/Shamyr.Urlik.Service/ Shamyr.Urlik.Service/
COPY src/Shamyr.Urlik.Database/ Shamyr.Urlik.Database/

WORKDIR Shamyr.Urlik.Service
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shamyr.Urlik.Service.dll"]
