#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["authService/authService.Api/authService.Api.csproj", "authService/authService.Api/"]
COPY ["authService/authService.Data/authService.Data.csproj", "authService/authService.Data/"]
COPY ["authService/authService.DomainModels/authService.DomainModels.csproj", "authService/authService.DomainModels/"]
COPY ["authService/authService.Application/authService.Application.csproj", "authService/authService.Application/"]
RUN dotnet restore "authService/authService.Api/authService.Api.csproj"
COPY . .
WORKDIR "/src/authService/authService.Api"
RUN dotnet build "authService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "authService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "authService.Api.dll"]