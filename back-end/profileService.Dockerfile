#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["profileService/profileService.API/profileService.API.csproj", "profileService/profileService.API/"]
COPY ["profileService/profileService.Data/profileService.Data.csproj", "profileService/profileService.Data/"]
COPY ["profileService/profileService.DomainModel/profileService.DomainModel.csproj", "profileService/profileService.DomainModel/"]
COPY ["profileService/profileService.Application/profileService.Application.csproj", "profileService/profileService.Application/"]
RUN dotnet restore "profileService/profileService.API/profileService.API.csproj"
COPY . .
WORKDIR "/src/profileService/profileService.API"
RUN dotnet build "profileService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "profileService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "profileService.API.dll"]