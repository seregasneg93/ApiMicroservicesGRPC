FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebMain/WebMain.csproj", "WebMain/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "WebMain/WebMain.csproj"
COPY . .
WORKDIR "/src/WebMain"
RUN dotnet publish "WebMain.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebMain.dll"]