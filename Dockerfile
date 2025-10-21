# Multi-stage build for Rhombus.WinFormsMcp
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS builder

WORKDIR /build

# Copy solution and project files
COPY Rhombus.WinFormsMcp.sln .
COPY src/Rhombus.WinFormsMcp.Server/Rhombus.WinFormsMcp.Server.csproj ./src/Rhombus.WinFormsMcp.Server/
COPY src/Rhombus.WinFormsMcp.TestApp/Rhombus.WinFormsMcp.TestApp.csproj ./src/Rhombus.WinFormsMcp.TestApp/
COPY tests/Rhombus.WinFormsMcp.Tests/Rhombus.WinFormsMcp.Tests.csproj ./tests/Rhombus.WinFormsMcp.Tests/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY src/ ./src/
COPY tests/ ./tests/

# Build release
RUN dotnet build -c Release --no-restore
RUN dotnet publish src/Rhombus.WinFormsMcp.Server/Rhombus.WinFormsMcp.Server.csproj -c Release -o /publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0-windowsservercore-ltsc2022

WORKDIR /app

# Copy published binaries
COPY --from=builder /publish .

# Set entry point
ENTRYPOINT ["Rhombus.WinFormsMcp.Server.exe"]

# Labels
LABEL org.opencontainers.image.title="Rhombus.WinFormsMcp"
LABEL org.opencontainers.image.description="WinForms automation MCP server with headless UI automation capabilities"
LABEL org.opencontainers.image.authors="rhom6us"
LABEL org.opencontainers.image.url="https://github.com/rhom6us/winforms-mcp"
LABEL org.opencontainers.image.source="https://github.com/rhom6us/winforms-mcp"
LABEL org.opencontainers.image.licenses="MIT"
