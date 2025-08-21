using PrinterCell.Shared.Models;
using System.Net.Http.Json;

namespace PrinterCell.Client.Services
{
    public class ApiArticoliService
    {
        private readonly HttpClient _http; // deve puntare alla BASE URL dell'API Server (LAN)

        public ApiArticoliService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Articolo?> CreateAsync(Articolo input, CancellationToken ct = default)
        {
            // Id lo gestisce il server
            input.Id = 0;

            var res = await _http.PostAsJsonAsync("api/articoli", input, ct);
            if (res.IsSuccessStatusCode)
                return await res.Content.ReadFromJsonAsync<Articolo>(cancellationToken: ct);

            var body = await res.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"Errore API {(int)res.StatusCode}: {body}");
        }
    }
}
