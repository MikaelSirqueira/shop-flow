using code.Models;
using Microsoft.EntityFrameworkCore;

namespace code.Data;

public class DataSeeder
{
    private readonly LojaDbContext _context;
    private readonly ClienteService _clienteService;
    private readonly ProdutoService _produtoService;
    private readonly VendaService _vendaService;

    public DataSeeder(LojaDbContext context, ClienteService clienteService, ProdutoService produtoService, VendaService vendaService)
    {
        _context = context;
        _clienteService = clienteService;
        _produtoService = produtoService;
        _vendaService = vendaService;
    }

    public async Task SeedAsync()
    {
        // Preenchendo Clientes
        if (!await _context.Clientes.AnyAsync())
        {
            var clientes = await _clienteService.GetClientesAsync();
            _context.Clientes.AddRange(clientes);
            await _context.SaveChangesAsync();

        }

        // Preenchendo Produtos
        if (!await _context.Produtos.AnyAsync())
        {
            var produtos = await _produtoService.GetProdutosAsync();
            _context.Produtos.AddRange(produtos);
            await _context.SaveChangesAsync();

        }

        // Preenchendo Vendas
        if (!await _context.Vendas.AnyAsync())
        {
            var vendas = await _vendaService.GetVendasAsync();
            _context.Vendas.AddRange(vendas);
            await _context.SaveChangesAsync();
        }
    }
}
