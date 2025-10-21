using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace fnWindowsMCP.Server;

/// <summary>
/// fnWindowsMCP - MCP Server for WinForms Automation
///
/// This server provides tools for automating WinForms applications in a headless manner.
/// It communicates via JSON-RPC over stdio (compatible with Claude Code).
/// </summary>
class Program
{
    private static AutomationServer? _server;

    static async Task Main(string[] args)
    {
        try
        {
            _server = new AutomationServer();
            await _server.RunAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
            Environment.Exit(1);
        }
    }
}

/// <summary>
/// Core MCP server implementation handling JSON-RPC communication
/// </summary>
class AutomationServer
{
    private readonly Dictionary<string, Func<JsonElement, Task<JsonElement>>> _tools;
    private int _nextId = 1;

    public AutomationServer()
    {
        _tools = new Dictionary<string, Func<JsonElement, Task<JsonElement>>>
        {
            // Element Tools
            { "find_element", FindElement },
            { "click_element", ClickElement },
            { "type_text", TypeText },
            { "set_value", SetValue },
            { "get_property", GetProperty },

            // Process Tools
            { "launch_app", LaunchApp },
            { "attach_to_process", AttachToProcess },
            { "close_app", CloseApp },

            // Validation Tools
            { "take_screenshot", TakeScreenshot },
            { "element_exists", ElementExists },
            { "wait_for_element", WaitForElement },

            // Interaction Tools
            { "drag_drop", DragDrop },
            { "send_keys", SendKeys },

            // Event Tools
            { "raise_event", RaiseEvent },
            { "listen_for_event", ListenForEvent },
        };
    }

    public async Task RunAsync()
    {
        var reader = Console.In;
        var writer = Console.Out;

        // Send initialization
        var initMessage = new
        {
            jsonrpc = "2.0",
            result = new
            {
                protocolVersion = "2024-11-05",
                capabilities = new
                {
                    tools = GetToolDefinitions()
                },
                serverInfo = new
                {
                    name = "fnWindowsMCP",
                    version = "1.0.0"
                }
            }
        };

        await writer.WriteLineAsync(JsonSerializer.Serialize(initMessage));
        await writer.FlushAsync();

        // Process incoming messages
        while (true)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(line))
                break;

            try
            {
                var request = JsonDocument.Parse(line).RootElement;
                var response = await ProcessRequest(request);
                await writer.WriteLineAsync(JsonSerializer.Serialize(response));
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                var error = new
                {
                    jsonrpc = "2.0",
                    error = new
                    {
                        code = -32603,
                        message = "Internal error",
                        data = new { details = ex.Message }
                    }
                };
                await writer.WriteLineAsync(JsonSerializer.Serialize(error));
                await writer.FlushAsync();
            }
        }
    }

    private async Task<object> ProcessRequest(JsonElement request)
    {
        if (!request.TryGetProperty("method", out var methodElement))
            throw new InvalidOperationException("Missing method");

        var method = methodElement.GetString();
        if (method == "initialize")
        {
            return new
            {
                jsonrpc = "2.0",
                id = request.TryGetProperty("id", out var id) ? id.GetInt32() : _nextId++,
                result = new
                {
                    protocolVersion = "2024-11-05",
                    capabilities = new
                    {
                        tools = GetToolDefinitions()
                    },
                    serverInfo = new
                    {
                        name = "fnWindowsMCP",
                        version = "1.0.0"
                    }
                }
            };
        }

        if (method == "tools/list")
        {
            return new
            {
                jsonrpc = "2.0",
                id = request.TryGetProperty("id", out var id) ? id.GetInt32() : _nextId++,
                result = new
                {
                    tools = GetToolDefinitions()
                }
            };
        }

        if (method == "tools/call")
        {
            if (!request.TryGetProperty("params", out var paramsElement))
                throw new InvalidOperationException("Missing params");

            if (!paramsElement.TryGetProperty("name", out var nameElement))
                throw new InvalidOperationException("Missing tool name");

            var toolName = nameElement.GetString();
            var toolArgs = paramsElement.TryGetProperty("arguments", out var args) ? args : default;

            if (!_tools.ContainsKey(toolName))
                throw new InvalidOperationException($"Unknown tool: {toolName}");

            var result = await _tools[toolName](toolArgs);

            return new
            {
                jsonrpc = "2.0",
                id = request.TryGetProperty("id", out var id) ? id.GetInt32() : _nextId++,
                result = new
                {
                    content = new[]
                    {
                        new
                        {
                            type = "text",
                            text = result.ToString()
                        }
                    }
                }
            };
        }

        throw new InvalidOperationException($"Unknown method: {method}");
    }

    private object GetToolDefinitions()
    {
        return new object[]
        {
            new
            {
                name = "find_element",
                description = "Find a UI element by AutomationId, Name, ClassName, or ControlType",
                inputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        automationId = new { type = "string", description = "AutomationId of the element" },
                        name = new { type = "string", description = "Name of the element" },
                        className = new { type = "string", description = "ClassName of the element" },
                        controlType = new { type = "string", description = "ControlType of the element" },
                        parent = new { type = "string", description = "Parent element path (optional)" }
                    }
                }
            },
            new
            {
                name = "click_element",
                description = "Click on a UI element",
                inputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        elementPath = new { type = "string", description = "Path or identifier of the element" },
                        doubleClick = new { type = "boolean", description = "Double-click if true" }
                    },
                    required = new[] { "elementPath" }
                }
            },
            new
            {
                name = "type_text",
                description = "Type text into a text field",
                inputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        elementPath = new { type = "string", description = "Path or identifier of the element" },
                        text = new { type = "string", description = "Text to type" },
                        clearFirst = new { type = "boolean", description = "Clear field before typing" }
                    },
                    required = new[] { "elementPath", "text" }
                }
            },
            new
            {
                name = "launch_app",
                description = "Launch a WinForms application",
                inputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        path = new { type = "string", description = "Path to the executable" },
                        arguments = new { type = "string", description = "Command-line arguments (optional)" },
                        workingDirectory = new { type = "string", description = "Working directory (optional)" }
                    },
                    required = new[] { "path" }
                }
            },
            new
            {
                name = "take_screenshot",
                description = "Take a screenshot of the application or element",
                inputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        outputPath = new { type = "string", description = "Path to save the screenshot" },
                        elementPath = new { type = "string", description = "Specific element to screenshot (optional)" }
                    },
                    required = new[] { "outputPath" }
                }
            }
        };
    }

    // Tool implementations - placeholders for now
    private async Task<JsonElement> FindElement(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> ClickElement(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> TypeText(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> SetValue(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> GetProperty(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> LaunchApp(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> AttachToProcess(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> CloseApp(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> TakeScreenshot(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> ElementExists(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> WaitForElement(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> DragDrop(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> SendKeys(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> RaiseEvent(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;

    private async Task<JsonElement> ListenForEvent(JsonElement args) =>
        JsonDocument.Parse("{}").RootElement;
}
