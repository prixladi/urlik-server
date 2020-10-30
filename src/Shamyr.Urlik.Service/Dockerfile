#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["shamyr-urlik-server/shamyr-urlik-server.csproj", "shamyr-urlik-server/"]
RUN dotnet restore "shamyr-urlik-server/shamyr-urlik-server.csproj"
COPY . .
WORKDIR "/src/shamyr-urlik-server"
RUN dotnet build "shamyr-urlik-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shamyr-urlik-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shamyr-urlik-server.dll"]