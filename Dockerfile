#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MajorTestOrientation/MajorTestOrientation.csproj", "MajorTestOrientation/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Entities/Entities.csproj", "Entities/"]
RUN dotnet restore "MajorTestOrientation/MajorTestOrientation.csproj"
COPY . .
WORKDIR "/src/MajorTestOrientation"
RUN dotnet build "MajorTestOrientation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MajorTestOrientation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MajorTestOrientation.dll"]