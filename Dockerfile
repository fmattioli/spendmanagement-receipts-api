#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /src
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/SpendManagement.ApiGateway.csproj", "SpendManagement.ApiGateway/"]
RUN dotnet restore "SpendManagement.ApiGateway/SpendManagement.ApiGateway.csproj"
COPY . .

RUN dotnet build "src/SpendManagement.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/SpendManagement.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpendManagement.ApiGateway.dll"]