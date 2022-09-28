#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["introduction-api.csproj", "introduction-api/"]
RUN dotnet restore "introduction-api/introduction-api.csproj"
RUN ls
COPY . ./introduction-api/
WORKDIR /src/introduction-api
RUN dotnet build "introduction-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "introduction-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "introduction-api.dll"]
