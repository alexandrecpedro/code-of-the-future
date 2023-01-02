#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Web Apps/MVC/MVC.csproj", "Web Apps/MVC/"]
COPY ["Services/Messages/Messages.csproj", "Services/Messages/"]
RUN dotnet restore "Web Apps/MVC/MVC.csproj"
COPY . .
WORKDIR "/src/Web Apps/MVC"
RUN dotnet build "MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVC.dll"]