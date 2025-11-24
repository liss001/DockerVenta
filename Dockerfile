# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore DockerVenta.csproj
RUN dotnet build DockerVenta.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish DockerVenta.csproj -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerVenta.dll"]
