FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY security/sso/SSO.API/SSO.API.csproj security/sso/SSO.API/
RUN dotnet restore security/sso/SSO.API/SSO.API.csproj
COPY . .
WORKDIR /src/security/sso/SSO.API
RUN dotnet build SSO.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SSO.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SSO.API.dll"]