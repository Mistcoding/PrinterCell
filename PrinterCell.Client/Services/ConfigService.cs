using System.Net.Http.Json;

namespace PrinterCell.Client.Services
{
    public class ClientConfig { public string ApiBaseUrl { get; set; } = ""; }

    public class ConfigService
    {
        private readonly HttpClient _static; // quello "statico" già registrato con BaseAddress = HostEnvironment
        public ClientConfig Config { get; private set; } = new();

        public ConfigService(HttpClient staticClient) { _static = staticClient; }

        public async Task LoadAsync()
            => Config = await _static.GetFromJsonAsync<ClientConfig>("appsettings.client.json") ?? new();
    }
}
