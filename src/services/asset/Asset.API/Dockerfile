FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY . .
WORKDIR /src/services/asset/Asset.API
RUN dotnet restore
RUN dotnet build --no-restore -c Release -o /app

# Unit/Functional Tests
#FROM build as unittest
#WORKDIR /src/services/..

#FROM build as functionaltest
#WORKDIR /src/services/..

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Asset.API.dll"]