using code.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace code.Data;

public class LojaDbContext : DbContext
{
    public DbSet<ClienteModel> Clientes { get; set; }
    public DbSet<ProdutoModel> Produtos { get; set; }
    public DbSet<VendaModel> Vendas { get; set; }

    public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options)
    { }
}
