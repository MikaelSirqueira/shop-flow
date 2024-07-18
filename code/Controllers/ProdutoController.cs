using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using code.Data;
using code.Models;
using code.Repository;

public class ProdutoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public IActionResult Index()
    {
        var produtos = _produtoRepository.GetAll().ToList();

        return View(produtos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ProdutoModel produto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Post(produto);
                TempData["MensagemSucesso"] = "Produto cadastrado com sucesso";
                return RedirectToAction("Index");
            }

            return View(produto);
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos cadastrar seu produto. Detalhes: {e.Message}";
            return RedirectToAction("Index");
        }

    }

    public IActionResult Edit(int id)
    {
        ProdutoModel produto = _produtoRepository.GetById(id);

        return View(produto);
    }

    [HttpPost]
    public IActionResult SaveEdit(ProdutoModel produto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Update(produto);
                TempData["MensagemSucesso"] = "produto alterado com sucesso";
                return RedirectToAction("Index");
            }

            return View("Edit", produto);
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos alterar seu produto. Detalhes: {e.Message}";
            return RedirectToAction("Edit");
        }
    }

    public IActionResult ConfirmDelete(int id)
    {
        ProdutoModel produto = _produtoRepository.GetById(id);

        return View(produto);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            bool isprodutoDeleted = _produtoRepository.Delete(id);

            if (isprodutoDeleted)
            {
                TempData["MensagemSucesso"] = "produto deletado com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "produto não foi deletado!";
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["MensagemErro"] = $"Não conseguimos deletar este produto. Detalhes: {e.Message}";
            return RedirectToAction("Index");
        }
    }
}
