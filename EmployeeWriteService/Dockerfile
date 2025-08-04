FROM mcr.microsoft.com/dotnet/sdk:6.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

COPY EmployeeWriteService/EmployeeWriteService.csproj ./EmployeeWriteService/
RUN dotnet restore ./EmployeeWriteService/EmployeeWriteService.csproj

COPY EmployeeWriteService/ ./EmployeeWriteService/
WORKDIR /src/EmployeeWriteService
RUN dotnet build -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2022 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EmployeeWriteService.dll"]