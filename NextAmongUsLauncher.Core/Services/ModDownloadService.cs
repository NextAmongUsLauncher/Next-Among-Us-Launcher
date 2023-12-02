namespace NextAmongUsLauncher.Core.Services;

public class ModDownloadService(HttpClient _client)
{
    private readonly HttpClient _httpClient = _client;
}