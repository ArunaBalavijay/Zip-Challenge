FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Zip.Challenge.Services.Identity/Zip.Challenge.Services.Identity.csproj", "src/Zip.Challenge.Services.Identity/"]
RUN dotnet restore "src/Zip.Challenge.Services.Identity/Zip.Challenge.Services.Identity.csproj"
COPY . .
WORKDIR "/src/src/Zip.Challenge.Services.Identity"
RUN dotnet build "Zip.Challenge.Services.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zip.Challenge.Services.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "Zip.Challenge.Services.Identity.dll"]