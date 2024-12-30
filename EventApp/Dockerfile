FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventApp.API/EventApp.API.csproj", "EventApp.API/"]
RUN dotnet restore "./EventApp.API/EventApp.API.csproj"
COPY . .
WORKDIR "/src/EventApp.API"
RUN dotnet build "./EventApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM build AS migrations
WORKDIR /src/EventApp.Infrastructure
COPY ["EventApp.Infrastructure/EventApp.Infrastructure.csproj", "EventApp.Infrastructure/"]
RUN dotnet restore "./EventApp.Infrastructure/EventApp.Infrastructure.csproj"
COPY . .

# RUN dotnet ef migrations add Init
# RUN dotnet ef database update --project "./Library.Infrastructure/Library.Infrastructure.csproj"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventApp.API.dll"]