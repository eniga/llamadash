# Llama Dashboard

A Blazor Server dashboard for managing and monitoring your llama.cpp instance.

## Features

- **Dashboard**: View quick stats including GPU usage, loaded model, token metrics, and performance
- **Devices**: Monitor GPU devices with memory usage and utilization
- **Models**: Manage loaded models with load/unload functionality
- **Stats**: View detailed performance metrics and statistics
- **Chat**: Interactive chat interface to communicate with your model
- **Settings**: Configure server connection and dashboard preferences

## Requirements

- .NET 8 SDK
- llama.cpp Server (running with OpenAI-compatible API)
- Modern web browser

## Quick Start

### Prerequisites

1. Ensure you have a running llama.cpp server with OpenAI-compatible API
2. Update `appsettings.json` with your server URL and API key if needed

### Development

```bash
# Navigate to the project directory
cd src/LlamaDashboard

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

The dashboard will be available at `http://localhost:5000`

### Docker

```bash
# Build and run with Docker Compose
docker-compose up --build

# View logs
docker compose logs -f llama-dashboard

# Stop
docker compose down
```

The dashboard will be available at `http://localhost:8081`.

**Note:** The docker-compose configuration assumes your llama.cpp server is already running and accessible at `https://ai.aradhel.dev/v1`. The dashboard container has GPU access for monitoring purposes.

## Configuration

Edit `appsettings.json` to configure:

- **LlamaCpp.ServerUrl**: URL of your llama.cpp server
- **LlamaCpp.ApiKey**: API key (if required)
- **Dashboard.RefreshInterval**: Data refresh interval in milliseconds
- **Dashboard.Theme**: UI theme (dark/light)

## Project Structure

```
src/LlamaDashboard/
├── Models/           # Data models
│   ├── Device.cs
│   ├── ModelInfo.cs
│   ├── Stats.cs
│   ├── ChatMessage.cs
│   └── Config.cs
├── Services/         # Service layer
│   ├── ILlamaCppService.cs
│   ├── LlamaCppService.cs
│   └── ConfigService.cs
├── Pages/            # Blazor pages
│   ├── Dashboard.razor
│   ├── Devices.razor
│   ├── Models.razor
│   ├── Stats.razor
│   ├── Chat.razor
│   └── Settings.razor
├── Shared/           # Shared components
│   ├── MainLayout.razor
│   └── NavMenu.razor
└── wwwroot/css/     # Styles
    └── site.css
```

## API Endpoints

The dashboard communicates with llama.cpp Server at the configured URL. Supported endpoints:

- `/v1/models` - List available models
- `/v1/models/{id}` - Load/unload models
- `/v1/stats` - Get performance statistics
- `/v1/devices` - Get GPU device information
- `/v1/chat/completions` - Chat completions
- `/health` - Health check

## Troubleshooting

### Connection Issues

1. Verify your llama.cpp server is running
2. Check the server URL in Settings
3. Ensure the API endpoint is accessible
4. Check browser console for errors

### Performance Issues

1. Reduce refresh interval in Settings
2. Check GPU memory usage
3. Monitor server logs

## License

MIT

Configuration is managed via `appsettings.json` or environment variables.

### appsettings.json

```json
{
  "LlamaCpp": {
    "Url": "https://ai.aradhel.dev/v1",
    "ApiKey": "",
    "RefreshInterval": 10000
  },
  "Dashboard": {
    "Name": "Llama Dashboard",
    "Theme": "dark"
  }
}
```

### Environment Variables

```bash
export LlamaCpp__Url=https://ai.aradhel.dev/v1
export LlamaCpp__ApiKey=your-api-key
```

## Project Structure

```
llamadashboard/
├── LlamaDashboard.sln
├── src/
│   └── LlamaDashboard/
│       ├── LlamaDashboard.csproj
│       ├── Program.cs
│       ├── appsettings.json
│       ├── Models/
│       │   ├── Device.cs
│       │   ├── Model.cs
│       │   ├── Stats.cs
│       │   ├── ChatMessage.cs
│       │   └── Config.cs
│       ├── Services/
│       │   ├── ILlamaCppService.cs
│       │   └── ConfigService.cs
│       ├── Pages/
│       │   ├── Dashboard.razor
│       │   ├── Devices.razor
│       │   ├── Models.razor
│       │   ├── Stats.razor
│       │   ├── Chat.razor
│       │   ├── Settings.razor
│       │   ├── _Host.cshtml
│       │   └── _Imports.razor
│       ├── Shared/
│       │   ├── MainLayout.razor
│       │   └── NavMenu.razor
│       └── wwwroot/
│           └── css/
│               └── site.css
├── Dockerfile
├── docker-compose.yml
└── README.md
```

## API Endpoints

The dashboard communicates with llama.cpp via its OpenAI-compatible API:

- `GET /v1/models` — List available models
- `POST /v1/models/load` — Load a model
- `POST /v1/models/unload` — Unload a model
- `GET /stats` — Get server statistics
- `POST /v1/chat/completions` — Chat completions

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Blazor Server, C#, Razor Components |
| Backend | ASP.NET Core 8, HttpClient |
| Styling | Custom CSS (dark theme) |
| Deployment | Docker, Docker Compose |
| API | OpenAI Compatible (llama.cpp) |

## Additional Features

- **Real-time Updates** — SignalR for live data refresh
- **Editable Settings** — Change server URL and other settings without restarting
- **Health Checks** — Monitor server connectivity
- **Responsive Layout** — Mobile-friendly design
- **Dark Theme** — Easy on the eyes for 24/7 monitoring

## Troubleshooting

### Dashboard shows "Disconnected"

- Verify the llama.cpp server URL in Settings
- Check network connectivity: `curl http://your-llm-server:8080/health`
- Ensure the server allows CORS if accessed from a different origin

### Models list is empty

- Ensure the llama.cpp server has models in its models directory
- Check that the server's `--model` or models directory is properly configured
- Try loading a model manually via the Models page

### Chat not responding

- Verify a model is loaded on the server
- Check server logs for inference errors
- Ensure the server allows chat completions

## License

MIT

## Acknowledgments

- [llama.cpp](https://github.com/ggerganov/llama.cpp) — High-performance LLM inference
- [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) — .NET web framework
- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) — Modern .NET runtime
# llamadash
