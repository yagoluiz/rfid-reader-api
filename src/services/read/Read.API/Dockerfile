FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY services/read/Read.API/Read.API.csproj services/read/Read.API/
RUN dotnet restore services/read/Read.API/Read.API.csproj
COPY . .
WORKDIR /src/services/read/Read.API
RUN dotnet build Read.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Read.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Read.API.dll"]
