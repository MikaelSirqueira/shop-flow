using code.Data;
using code.Models;

namespace code.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly LojaDbContext _context;

    public ClienteRepository(LojaDbContext lojaDbContext)
    {
        _context = lojaDbContext;
    }

    public List<ClienteModel> GetAll()
    {
        return _context.Clientes.ToList();
    }

    public ClienteModel GetById(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(c => c.IdCliente == id);

        if (cliente == null) { throw new Exception(); }

        return cliente;
    }

    public ClienteModel Post(ClienteModel cliente)
    {
        if (cliente == null) 
        {
            throw new ArgumentNullException(nameof(cliente));
        };

        _context.Clientes.Add(cliente);
        _context.SaveChanges();    
        
        return cliente;
    }

    public ClienteModel Update(ClienteModel cliente)
    {
        ClienteModel clienteModel = GetById(cliente.IdCliente);

        if ( clienteModel == null ) { throw new Exception("Erro na atualização do cliente!"); }

        clienteModel.NmCliente = cliente.NmCliente;
        clienteModel.Cidade = cliente.Cidade;

        _context.Clientes.Update(clienteModel);
        _context.SaveChanges();

        return clienteModel;
    }

    public bool Delete(int id)
    {
        ClienteModel clienteModel = GetById(id);

        if (clienteModel == null) { throw new Exception("Erro ao deletar cliente!"); }

        _context.Clientes.Remove(clienteModel);
        _context.SaveChanges();

        return true;
    }
}
