using code.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using code.Data;

public class VendaService
{
    private readonly HttpClient _httpClient;
    private readonly LojaDbContext _context;
    private const string VendasEndpoint = "https://camposdealer.dev/Sites/TesteAPI/venda";

    public VendaService(HttpClient httpClient, LojaDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<List<VendaModel>> GetVendasAsync()
    {
        var response = await _httpClient.GetAsync(VendasEndpoint);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        // Removendo caracteres desnecessários e desserializando o JSON
        var sanitizedContent = responseContent.Trim('"').Replace("\\", string.Empty);
        var vendas = JsonConvert.DeserializeObject<List<VendaModel>>(sanitizedContent);

        if (vendas != null)
        {
            foreach (var venda in vendas)
            {
                // Garantir que o Cliente e Produto existam no banco de dados
                var cliente = await _context.Clientes.FindAsync(venda.IdCliente);
                if (cliente != null)
                {
                    venda.Cliente = cliente;
                }

                var produto = await _context.Produtos.FindAsync(venda.IdProduto);
                if (produto != null)
                {
                    venda.Produto = produto;
                }
            }
        }

        return vendas ?? new List<VendaModel>();
    }

    public async Task SaveVendasAsync(List<VendaModel> vendas)
    {
        foreach (var venda in vendas)
        {
            // Verifica se o cliente e o produto existem antes de salvar a venda
            var cliente = await _context.Clientes.FindAsync(venda.IdCliente);
            var produto = await _context.Produtos.FindAsync(venda.IdProduto);

            if (cliente != null && produto != null)
            {
                // Cliente e Produto existem, então salva a venda
                _context.Vendas.Add(venda);
            }
            else
            {
                // Caso contrário, você pode lançar uma exceção ou logar o erro
                throw new Exception($"Cliente ou Produto não encontrados para a venda {venda.IdVenda}");
            }
        }

        await _context.SaveChangesAsync();
    }
}
