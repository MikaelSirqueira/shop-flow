using code.Data;
using code.Models;

namespace code.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly LojaDbContext _context;

    public ProdutoRepository(LojaDbContext lojaDbContext)
    {
        _context = lojaDbContext;
    }


    public List<ProdutoModel> GetAll()
    {
        return _context.Produtos.ToList();
    }

    public ProdutoModel GetById(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(c => c.IdProduto == id);

        if (produto == null) { throw new Exception(); }

        return produto;
    }

    public ProdutoModel Post(ProdutoModel produto)
    {
        if (produto == null)
        {
            throw new ArgumentNullException(nameof(produto));
        };

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return produto;
    }

    public ProdutoModel Update(ProdutoModel produto)
    {
        ProdutoModel ProdutoModel = GetById(produto.IdProduto);

        if (ProdutoModel == null) { throw new Exception("Erro na atualização do produto!"); }

        ProdutoModel.DscProduto = produto.DscProduto;
        ProdutoModel.VlrUnitario = produto.VlrUnitario;

        _context.Produtos.Update(ProdutoModel);
        _context.SaveChanges();

        return ProdutoModel;
    }

    public bool Delete(int id)
    {
        ProdutoModel ProdutoModel = GetById(id);

        if (ProdutoModel == null) { throw new Exception("Erro ao deletar produto!"); }

        _context.Produtos.Remove(ProdutoModel);
        _context.SaveChanges();

        return true;
    }
}
