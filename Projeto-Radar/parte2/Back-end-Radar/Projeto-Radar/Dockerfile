FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 8080/tcp
WORKDIR /App
COPY --from=build-env /App/out .
ENV ASPNETCORE_URLS="http://*:8080"
ENTRYPOINT ["dotnet", "Projeto-Radar.dll"]

