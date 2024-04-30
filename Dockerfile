#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /src
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/SpendManagement.Receipts.Api.csproj", "SpendManagement.Receipts.Api/"]
RUN dotnet restore "SpendManagement.Receipts.Api/SpendManagement.Receipts.Api.csproj"
COPY . .

RUN dotnet build "src/SpendManagement.Receipts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/SpendManagement.Receipts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpendManagement.Receipts.Api.dll"]