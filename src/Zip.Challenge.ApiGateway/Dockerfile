FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Zip.Challenge.ApiGateway/Zip.Challenge.ApiGateway.csproj", "src/Zip.Challenge.ApiGateway/"]
RUN dotnet restore "src/Zip.Challenge.ApiGateway/Zip.Challenge.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/Zip.Challenge.ApiGateway"
RUN dotnet build "Zip.Challenge.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zip.Challenge.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "Zip.Challenge.ApiGateway.dll"]