FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 5112

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["appleEarStore.WebApi/appleEarStore.WebApi.csproj", "appleEarStore.WebApi/"]
COPY ["Data", "Data/"]
COPY ["Database", "Database/"]
RUN dotnet restore "appleEarStore.WebApi/appleEarStore.WebApi.csproj"
COPY ["appleEarStore.WebApi", "appleEarStore.WebApi/"]
WORKDIR "/src/appleEarStore.WebApi"
RUN dotnet build "appleEarStore.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "appleEarStore.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "appleEarStore.WebApi.dll"]
