#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SourceFuseSampleAPI/SourceFuseSampleAPI.csproj", "SourceFuseSampleAPI/"]
RUN dotnet restore "SourceFuseSampleAPI/SourceFuseSampleAPI.csproj"
COPY . .
WORKDIR "/src/SourceFuseSampleAPI"
RUN dotnet build "SourceFuseSampleAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SourceFuseSampleAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SourceFuseSampleAPI.dll"]