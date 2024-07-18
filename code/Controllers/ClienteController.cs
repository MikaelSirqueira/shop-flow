using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using code.Data;
using code.Models;
using code.Repository;

public class ClienteController : Controller
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public IActionResult Index()
    {
        var clientes = _clienteRepository.GetAll().ToList();


        return View(clientes);
    }

    // GET: Cliente/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ClienteModel cliente)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Post(cliente);
                TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso";
                return RedirectToAction("Index");
            }

            return View(cliente);
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos cadastrar seu cliente. Detalhes: {e.Message}";
            return RedirectToAction("Index");
        }

    }


    public IActionResult Edit(int id)
    {
        ClienteModel cliente = _clienteRepository.GetById(id);

        return View(cliente);
    }

    [HttpPost]
    public IActionResult SaveEdit(ClienteModel cliente)    {       

        try
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Update(cliente);
                TempData["MensagemSucesso"] = "Cliente alterado com sucesso";
                return RedirectToAction("Index");
            }

            return View("Edit", cliente);
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos alterar seu cliente. Detalhes: {e.Message}";
            return RedirectToAction("Edit");
        }
    }

    public IActionResult ConfirmDelete(int id)
    {
        ClienteModel cliente = _clienteRepository.GetById(id);

        return View(cliente);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            bool isClienteDeleted = _clienteRepository.Delete(id);

            if (isClienteDeleted)
            {
                TempData["MensagemSucesso"] = "Cliente deletado com sucesso!";                
            }
            else
            {
                TempData["MensagemErro"] = "Cliente não foi deletado!";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos deletar seu contato. Detalhes: {e.Message}";
            return RedirectToAction("Index");
        }
    }
}
