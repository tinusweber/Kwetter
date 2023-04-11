#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfileService/ProfileService.Api/ProfileService.Api.csproj", "ProfileService/ProfileService.Api/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
COPY ["ProfileService/ProfileService.Application/ProfileService.Application.csproj", "ProfileService/ProfileService.Application/"]
COPY ["ProfileService/ProfileService.Data/ProfileService.Data.csproj", "ProfileService/ProfileService.Data/"]
COPY ["MessagingModels/MessagingModels.csproj", "MessagingModels/"]
RUN dotnet restore "ProfileService/ProfileService.Api/ProfileService.Api.csproj"
COPY . .
WORKDIR "/src/ProfileService/ProfileService.Api"
RUN dotnet build "ProfileService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfileService.Api.dll"]