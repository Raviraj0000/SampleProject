FROM mcr.microsoft.com/dotnet/sdk:9.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

COPY EmployeeReadService/EmployeeReadService.csproj ./EmployeeReadService/
RUN dotnet restore ./EmployeeReadService/EmployeeReadService.csproj

COPY EmployeeReadService/ ./EmployeeReadService/
WORKDIR /src/EmployeeReadService
RUN dotnet build -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0-windowsservercore-ltsc2022 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EmployeeReadService.dll"]