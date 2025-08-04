
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

# Copy only the .csproj first
COPY EmployeeWriteService/EmployeeWriteService.csproj ./EmployeeWriteService/

# Restore dependencies
RUN dotnet restore "./EmployeeWriteService/EmployeeWriteService.csproj"

# Copy the rest of the source
COPY EmployeeWriteService/ ./EmployeeWriteService/

WORKDIR /src/EmployeeWriteService
RUN dotnet build -c Release -o /app