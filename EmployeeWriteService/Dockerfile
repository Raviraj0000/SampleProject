
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2022 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeWriteService.csproj", "EmployeeWriteService/"]
RUN dotnet restore "EmployeeWriteService.csproj"
COPY . .
WORKDIR "/src/EmployeeWriteService"
RUN dotnet publish -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "EmployeeWriteService.dll"]
