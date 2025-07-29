# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/EmployeeReadService.csproj", "EmployeeReadService/"]
RUN dotnet restore "EmployeeReadService/EmployeeReadService.csproj"
COPY . .
WORKDIR "/src/EmployeeReadService"
RUN dotnet publish -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "EmployeeReadService.dll"]