using code.Models;

namespace code.Repository;

public interface IProdutoRepository
{
    List<ProdutoModel> GetAll();
    ProdutoModel GetById(int id);
    ProdutoModel Post(ProdutoModel cliente);
    ProdutoModel Update(ProdutoModel cliente);
    bool Delete(int id);

}
