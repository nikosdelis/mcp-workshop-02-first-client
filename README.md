# 02-first-client

A small .NET 9 console client that demonstrates starting a local MCP-compatible server via stdio, connecting with the ModelContextProtocol client, listing available tools, and invoking the "multiply" tool.

## Prerequisites
- .NET SDK 9.0 or later (https://dotnet.microsoft.com/download)
- The companion server project built (../01-first-server)
- Git (optional)

## Overview
Program.cs:
- Loads configuration from environment variables and user secrets.
- Creates a StdioClientTransport configured with a Command pointing to the server executable.
- Starts an McpClient, lists available tools, and calls the "multiply" tool with inputs a=5 and b=7.
- Prints the tool result to the console.

## Build
From the repository root, build both projects (debug):
```bash
# build the server first (adjust path if you renamed the server folder)
cd 01-first-server
dotnet build -c Debug

# then build the client
cd ../02-first-client
dotnet build -c Debug
```

## Configure the client
By default Program.cs uses a placeholder path:
Command = "<YOU BASEPATH>/mcp-workshop/01-first-server/bin/Debug/net9.0/01-first-server"

Update Program.cs to point to the built server executable (absolute path), or modify the transport initialization to read the command path from an environment variable or user secret. Example (bash):
```bash
export MCP_SERVER_CMD="/full/path/to/01-first-server/bin/Debug/net9.0/01-first-server"
# or edit Program.cs to replace the placeholder with the above path
```
On Unix/macOS ensure the server binary has execute permissions:
```bash
chmod +x /full/path/to/01-first-server/bin/Debug/net9.0/01-first-server
```

## Run
Run the client from the repository root or the project folder:
```bash
dotnet run --project 02-first-client
```
The client will start the configured server via stdio, enumerate tools, and invoke the "multiply" tool. Expected console output includes the connected tool(s) and a line similar to:
Result: 35

## Troubleshooting
- If the client fails to start the server, verify the Command path and execution permissions.
- Check that the server was built for net9.0 and the path matches the built binary location.
- Use dotnet --info to confirm the installed SDK version.
- If configuration is missing (API keys, secrets), set environment variables or user secrets as appropriate; Program.cs reads both sources.

## Project structure
- Program.cs — application entry point (connects to MCP server and demonstrates a tool call)
- README.md — this file
- Other source files under the project folder

## Contributing
Open an issue or submit a PR with improvements or fixes.

## License
Specify the project license here (e.g., MIT).