FROM mcr.microsoft.com/dotnet/sdk:9.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

COPY UserService/UserService.csproj ./UserService/
RUN dotnet restore ./UserService/UserService.csproj

COPY UserService/ ./UserService/
WORKDIR /src/UserService
RUN dotnet build -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2022 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "UserService.dll"]