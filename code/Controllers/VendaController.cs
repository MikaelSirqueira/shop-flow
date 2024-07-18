using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using code.Models;
using code.Repository;

public class VendaController : Controller
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;

    public VendaController(IVendaRepository vendaRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
    {
        _vendaRepository = vendaRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
    }

    public IActionResult Index(string searchString)
    {
        var vendas = _vendaRepository.GetAll();

        if (!string.IsNullOrEmpty(searchString))
        {
            vendas = vendas.Where(v => v.Cliente.NmCliente.Contains(searchString) || v.Produto.DscProduto.Contains(searchString)).ToList();
        }

        return View(vendas);
    }

    public IActionResult Create()
    {
        ViewBag.Clientes = _clienteRepository.GetAll();
        ViewBag.Produtos = _produtoRepository.GetAll();
        return View();
    }

    [HttpPost]
    public IActionResult Create(VendaModel venda)
    {
        try
        {                         
            _vendaRepository.Post(venda);
            TempData["MensagemSucesso"] = "Venda cadastrada com sucesso";
            return RedirectToAction("Index");            

            //ViewBag.Clientes = _clienteRepository.GetAll();
            //ViewBag.Produtos = _produtoRepository.GetAll();
            //return View(venda);
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos cadastrar sua venda. Detalhes: {e.Message}";
            return RedirectToAction("Index");
        }
    }

    public IActionResult Edit(int id)
    {
        VendaModel venda = _vendaRepository.GetById(id);
        ViewBag.Clientes = _clienteRepository.GetAll();
        ViewBag.Produtos = _produtoRepository.GetAll();
        return View(venda);
    }

    [HttpPost]
    public IActionResult SaveEdit(VendaModel venda)
    {
        try
        {
            if (ModelState.IsValid)
            {                
                _vendaRepository.Update(venda);
                TempData["MensagemSucesso"] = "Venda alterada com sucesso";
                return RedirectToAction("Index");
            }

            ViewBag.Clientes = _clienteRepository.GetAll();
            ViewBag.Produtos = _produtoRepository.GetAll();
            return View("Edit", venda);
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos alterar sua venda. Detalhes: {e.Message}";
            return RedirectToAction("Edit");
        }
    }

    public IActionResult ConfirmDelete(int id)
    {
        VendaModel venda = _vendaRepository.GetById(id);
        return View(venda);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            bool isVendaDeleted = _vendaRepository.Delete(id);

            if (isVendaDeleted)
            {
                TempData["MensagemSucesso"] = "Venda deletada com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Venda não foi deletada!";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos deletar esta venda. Detalhes: {e.Message}";
            return RedirectToAction("Index");
        }
    }
}
