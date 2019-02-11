FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY services/log/Log.API/Log.API.csproj services/log/Log.API/
RUN dotnet restore services/log/Log.API/Log.API.csproj
COPY . .
WORKDIR /src/services/log/Log.API
RUN dotnet build Log.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Log.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Log.API.dll"]
