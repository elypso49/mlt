FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["mlt.api/mlt.api.csproj", "mlt.api/"]

RUN dotnet restore "mlt.api/mlt.api.csproj"
COPY . .

WORKDIR "/src/mlt.api"
RUN dotnet build "mlt.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mlt.api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/Uploads && chmod -R 777 /app/Uploads

ENTRYPOINT ["dotnet", "mlt.api.dll"]