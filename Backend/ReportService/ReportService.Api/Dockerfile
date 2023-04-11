#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReportService/ReportService.Api/ReportService.Api.csproj", "ReportService/ReportService.Api/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
COPY ["MessagingModels/MessagingModels.csproj", "MessagingModels/"]
COPY ["ReportService/ReportService.Application/ReportService.Application.csproj", "ReportService/ReportService.Application/"]
COPY ["ReportService/ReportService.Data/ReportService.Data.csproj", "ReportService/ReportService.Data/"]
COPY ["ReportService/ReportService.DomainModels/ReportService.DomainModels.csproj", "ReportService/ReportService.DomainModels/"]
RUN dotnet restore "ReportService/ReportService.Api/ReportService.Api.csproj"
COPY . .
WORKDIR "/src/ReportService/ReportService.Api"
RUN dotnet build "ReportService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.Api.dll"]