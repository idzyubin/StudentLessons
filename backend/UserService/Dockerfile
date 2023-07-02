FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

WORKDIR /app

# COPY .sln file
COPY *.sln .

# COPY .csproj file
COPY src/UserService.Host/*.csproj ./src/UserService.Host/
COPY src/UserService.Domain/*.csproj ./src/UserService.Domain/
COPY src/UserService.Infrastructure/*.csproj ./src/UserService.Infrastructure/

# Restore dependencies
RUN dotnet restore -r alpine-x64

COPY src/UserService.Host/. ./src/UserService.Host/
COPY src/UserService.Domain/. ./src/UserService.Domain/
COPY src/UserService.Infrastructure/. ./src/UserService.Infrastructure/

# Build project
RUN dotnet build

# Publish result artifact
RUN dotnet publish -c Release -o /out -r alpine-x64

# Final stage/image
FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine AS publish

WORKDIR /app
COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["./UserService.Host"]