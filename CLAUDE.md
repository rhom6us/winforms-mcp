# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Rhombus.WinFormsMcp** is a Model Context Protocol (MCP) server that provides headless automation for WinForms applications using FlaUI with the UIA2 backend. The project structure follows the renamed pattern from `fnWindowsMCP` to `Rhombus.WinFormsMcp`.

## Build Commands

```bash
# Build the solution
dotnet build Rhombus.WinFormsMcp.sln

# Build for release
dotnet build Rhombus.WinFormsMcp.sln --configuration Release

# Restore dependencies
dotnet restore Rhombus.WinFormsMcp.sln

# Publish the server
dotnet publish src/Rhombus.WinFormsMcp.Server/Rhombus.WinFormsMcp.Server.csproj -c Release -o publish
```

## Test Commands

```bash
# Run all tests
dotnet test Rhombus.WinFormsMcp.sln

# Run tests with detailed output
dotnet test Rhombus.WinFormsMcp.sln --logger "console;verbosity=detailed"

# Run tests with code coverage
dotnet test Rhombus.WinFormsMcp.sln --collect:"XPlat Code Coverage"

# Run tests for release configuration
dotnet test Rhombus.WinFormsMcp.sln --configuration Release --no-build
```

## Run Commands

```bash
# Run the MCP server
dotnet run --project src/Rhombus.WinFormsMcp.Server/Rhombus.WinFormsMcp.Server.csproj

# Run the test WinForms application
dotnet run --project src/Rhombus.WinFormsMcp.TestApp/Rhombus.WinFormsMcp.TestApp.csproj
```

## Package Commands

```bash
# Create NuGet package (auto-generated on build)
dotnet build src/Rhombus.WinFormsMcp.Server/Rhombus.WinFormsMcp.Server.csproj -c Release

# Pack explicitly
dotnet pack src/Rhombus.WinFormsMcp.Server/Rhombus.WinFormsMcp.Server.csproj -c Release
```

## Architecture

### Core Components

1. **Rhombus.WinFormsMcp.Server** (src/Rhombus.WinFormsMcp.Server/)
   - `Program.cs`: MCP server implementation with JSON-RPC 2.0 over stdio transport. Contains 14 tool implementations and SessionManager for element caching.
   - `Automation/AutomationHelper.cs`: Core FlaUI wrapper with 25+ automation methods. Provides process management, element discovery, UI interaction, and validation capabilities.

2. **Rhombus.WinFormsMcp.TestApp** (src/Rhombus.WinFormsMcp.TestApp/)
   - Sample WinForms application with various controls for testing automation capabilities.

3. **Rhombus.WinFormsMcp.Tests** (tests/Rhombus.WinFormsMcp.Tests/)
   - NUnit test suite covering AutomationHelper functionality, process lifecycle, element operations, and resource cleanup.

### Key Technical Decisions

- **Framework**: .NET 8.0 Windows-specific (net8.0-windows) for WinForms compatibility
- **UI Automation**: FlaUI 4.0.0 with UIA2 backend for maximum WinForms compatibility without visual requirements
- **Testing**: NUnit 3.14.0 with Moq for mocking
- **Protocol**: MCP with stdio transport, single-line JSON-RPC 2.0 messages
- **Package Distribution**: NuGet (Rhombus.WinFormsMcp), NPM (@rhom6us/winforms-mcp)

### MCP Tools Available

The server implements 14 tools via JSON-RPC:
- Process Management: `launch_app`, `attach_to_process`, `close_app`
- Element Discovery: `find_element`, `element_exists`, `wait_for_element`
- UI Interaction: `click_element`, `type_text`, `set_value`, `drag_drop`, `send_keys`
- Property Access: `get_property`
- Validation: `take_screenshot`
- Future/Placeholder: `raise_event`, `listen_for_event` (not yet implemented)

### Session Management

The server maintains session state across tool calls:
- Cached automation elements with generated IDs (elem_1, elem_2, etc.)
- Active AutomationHelper instance
- Process tracking with PIDs

### Error Handling

- All operations wrapped in try-catch blocks
- Default timeout: 5000ms for find operations, 10000ms for async waits
- Retry mechanism: 100ms intervals for element discovery
- Resource cleanup via IDisposable pattern

## Version Management

Version is managed in the `VERSION` file. The publish workflow automatically bumps patch versions on master commits.

## CI/CD

- **CI**: Runs on Windows, builds, tests on push/PR to master/main
- **Publish**: Auto-bumps version, creates GitHub release, publishes to NuGet/NPM
- **Docker**: Configuration available but currently disabled due to buildkit compatibility issues

## Important Notes

1. The project was renamed from `fnWindowsMCP` to `Rhombus.WinFormsMcp` - all namespaces and project references use the new naming.
2. Git status shows multiple deleted files from the old `fnWindowsMCP` structure - these deletions represent the rename refactoring.
3. The solution file is `Rhombus.WinFormsMcp.sln` (not fnWindowsMCP.sln).
4. NuGet package ID is `Rhombus.WinFormsMcp`, NPM package is `@rhom6us/winforms-mcp`.
5. All project files use the `Rhombus.WinFormsMcp` namespace prefix.