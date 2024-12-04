using code.Models;
using Newtonsoft.Json;

public class ProdutoService
{
    private readonly HttpClient _httpClient;
    private const string ProdutosEndpoint = "https://camposdealer.dev/Sites/TesteAPI/produto";

    public ProdutoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProdutoModel>> GetProdutosAsync()
    {
        var response = await _httpClient.GetAsync(ProdutosEndpoint);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        // Removendo caracteres desnecessários e desserializando o JSON
        var sanitizedContent = responseContent.Trim('"').Replace("\\", string.Empty);
        var produtos = JsonConvert.DeserializeObject<List<ProdutoModel>>(sanitizedContent);

        return produtos ?? new List<ProdutoModel>();
    }
}
