FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4187
EXPOSE 44315

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY gateway/ocelot/Ocelot.API/Ocelot.API.csproj gateway/ocelot/Ocelot.API/
RUN dotnet restore gateway/ocelot/Ocelot.API/Ocelot.API.csproj
COPY . .
WORKDIR /src/gateway/ocelot/Ocelot.API
RUN dotnet build Ocelot.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Ocelot.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ocelot.API.dll"]
