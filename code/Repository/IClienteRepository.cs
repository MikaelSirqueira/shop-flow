using code.Models;

namespace code.Repository;

public interface IClienteRepository
{
    List<ClienteModel> GetAll();
    ClienteModel GetById(int id);
    ClienteModel Post(ClienteModel cliente);
    ClienteModel Update(ClienteModel cliente);
    bool Delete(int id);

}
