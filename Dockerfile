# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Install SQLite native library
RUN apt-get update && apt-get install -y libsqlite3-0 && rm -rf /var/lib/apt/lists/*

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the csproj explicitly
COPY ./src/Service.Catalogue.Api/Service.Catalogue.Api.csproj ./Service.Catalogue.Api/Service.Catalogue.Api.csproj

# Restore
RUN dotnet restore ./Service.Catalogue.Api/Service.Catalogue.Api.csproj

# Copy all project files
COPY ./src/Service.Catalogue.Api ./Service.Catalogue.Api
#COPY ./src/Service.Catalogue.Api/**/* ./Service.Catalogue.Api/

# Optionally copy other solution-level files if needed
# COPY *.sln ./

# Debug listing
RUN echo "Listing /src:" && ls -R /src
RUN echo "Listing /src/Service.Catalogue.Api:" && ls /src/Service.Catalogue.Api

# Work in project folder
WORKDIR /src/Service.Catalogue.Api

# Build
RUN dotnet build Service.Catalogue.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Service.Catalogue.Api
RUN dotnet publish Service.Catalogue.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Catalogue.Api.dll"]