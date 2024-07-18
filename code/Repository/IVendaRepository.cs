using code.Models;

namespace code.Repository;

public interface IVendaRepository
{
    List<VendaModel> GetAll();
    VendaModel GetById(int id);
    VendaModel Post(VendaModel cliente);
    VendaModel Update(VendaModel cliente);
    bool Delete(int id);

}
