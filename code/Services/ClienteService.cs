using code.Models;
using Newtonsoft.Json;

public class ClienteService
{
    private readonly HttpClient _httpClient;
    private const string ClientesEndpoint = "https://camposdealer.dev/Sites/TesteAPI/cliente";

    public ClienteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ClienteModel>> GetClientesAsync()
    {
        var response = await _httpClient.GetAsync(ClientesEndpoint);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        // Removendo caracteres desnecessários e desserializando o JSON
        var sanitizedContent = responseContent.Trim('"').Replace("\\", string.Empty);
        var clientes = JsonConvert.DeserializeObject<List<ClienteModel>>(sanitizedContent);

        return clientes ?? new List<ClienteModel>();
    }
}
