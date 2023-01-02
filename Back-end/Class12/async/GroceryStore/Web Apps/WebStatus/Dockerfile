#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Web Apps/WebStatus/WebStatus.csproj", "Web Apps/WebStatus/"]
RUN dotnet restore "Web Apps/WebStatus/WebStatus.csproj"
COPY . .
WORKDIR "/src/Web Apps/WebStatus"
RUN dotnet build "WebStatus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebStatus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStatus.dll"]