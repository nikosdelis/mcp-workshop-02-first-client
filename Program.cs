using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Client;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using System.Diagnostics;
using System.Net;
using System.Text;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

var clientTransport = new StdioClientTransport(new()
{
    Name = "Mcp Demo Server",
    //windows    Command = $"{Path.Combine(AppContext.BaseDirectory, "../../../../", "01-first-server/bin/Debug/net9.0/01-first-server")}", 
    Command = $"<YOU BASEPATH>/mcp-workshop/01-first-server/bin/Debug/net9.0/01-first-server",
    Arguments = []
});

Console.WriteLine("Setting up stdio transport...");

await using var mcpClient = await McpClientFactory.CreateAsync(clientTransport);

Console.WriteLine("Listing tools...");

var tools = await mcpClient.ListToolsAsync();

foreach (var tool in tools)
{
    Console.WriteLine($"Connected to server with tools: {tool.Name}");
}

var result = await mcpClient.CallToolAsync(
    "multiply",
    new Dictionary<string, object?>
    {
        { "a", 5 },
        { "b", 7 }
    },
    cancellationToken: CancellationToken.None
);

Console.WriteLine("Result: " + ((TextContentBlock)result.Content[0]).Text);