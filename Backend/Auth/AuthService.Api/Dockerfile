#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Auth/AuthService.Api/AuthService.Api.csproj", "Auth/AuthService.Api/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
COPY ["MessagingModels/MessagingModels.csproj", "MessagingModels/"]
COPY ["Auth/AuthService.Application/AuthService.Application.csproj", "Auth/AuthService.Application/"]
COPY ["Auth/AuthService.Model/AuthService.Model.csproj", "Auth/AuthService.Model/"]
COPY ["Auth/AuthService.Data/AuthService.Data.csproj", "Auth/AuthService.Data/"]
RUN dotnet restore "Auth/AuthService.Api/AuthService.Api.csproj"
COPY . .
WORKDIR "/src/Auth/AuthService.Api"
RUN dotnet build "AuthService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.Api.dll"]