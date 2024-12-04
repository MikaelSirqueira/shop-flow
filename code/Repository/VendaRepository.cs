using code.Data;
using code.Models;
using Microsoft.EntityFrameworkCore;

namespace code.Repository
{
    public class VendaRepository : IVendaRepository
    {
        private readonly LojaDbContext _context;

        public VendaRepository(LojaDbContext lojaDbContext)
        {
            _context = lojaDbContext;
        }

        public List<VendaModel> GetAll()
        {
            return _context
                .Vendas
                .Where(x => x.Cliente.IdCliente == x.IdCliente && x.Produto.IdProduto == x.IdProduto)
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .ToList();
        }

        public VendaModel GetById(int id)
        {
            var venda = _context
                .Vendas
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .FirstOrDefault(v => v.IdVenda == id);

            if (venda == null) { throw new Exception("Venda não encontrada."); }

            return venda;
        }

        public VendaModel Post(VendaModel venda)
        {
            if (venda == null)
            {
                throw new ArgumentNullException(nameof(venda));
            };

            venda.Produto = _context.Produtos.Where(x => x.IdProduto == venda.IdProduto).FirstOrDefault()!;
            venda.Cliente = _context.Clientes.Where(x => x.IdCliente == venda.IdCliente).FirstOrDefault()!;

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            return venda;
        }

        public VendaModel Update(VendaModel venda)
        {
            VendaModel vendaModel = GetById(venda.IdVenda);

            if (vendaModel == null) { throw new Exception("Erro na atualização da venda!"); }

            vendaModel.IdCliente = venda.IdCliente;
            vendaModel.IdProduto = venda.IdProduto;
            vendaModel.QtdVenda = venda.QtdVenda;
            vendaModel.VlrUnitarioVenda = venda.VlrUnitarioVenda;
            vendaModel.DthVenda = venda.DthVenda;
            vendaModel.Produto = _context.Produtos.Where(x => x.IdProduto == venda.IdProduto).FirstOrDefault()!;
            vendaModel.Cliente = _context.Clientes.Where(x => x.IdCliente == venda.IdCliente).FirstOrDefault()!;

            _context.Vendas.Update(vendaModel);
            _context.SaveChanges();

            return vendaModel;
        }

        public bool Delete(int id)
        {
            VendaModel vendaModel = GetById(id);

            if (vendaModel == null) { throw new Exception("Erro ao deletar venda!"); }

            _context.Vendas.Remove(vendaModel);
            _context.SaveChanges();

            return true;
        }
    }
}
