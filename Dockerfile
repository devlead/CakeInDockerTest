FROM mcr.microsoft.com/dotnet/aspnet:9.0.0-alpine3.20 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0.100-alpine3.20 AS build
ARG BUILD_VERSION=1.0.0
WORKDIR /repo
 
COPY [".", "."]

RUN dotnet tool restore
RUN dotnet cake --target="Publish" --build-version="${BUILD_VERSION}"

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .