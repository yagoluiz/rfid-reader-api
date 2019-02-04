FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 52435
EXPOSE 44358

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY services/monitoring/Monitoring.API/Monitoring.API.csproj services/monitoring/Monitoring.API/
RUN dotnet restore services/monitoring/Monitoring.API/Monitoring.API.csproj
COPY . .
WORKDIR /src/services/monitoring/Monitoring.API
RUN dotnet build Monitoring.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Monitoring.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Monitoring.API.dll"]