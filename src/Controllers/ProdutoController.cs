using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Raiz.Data;
using Raiz.Models;
using Raiz.ViewModels;

namespace Raiz.Controllers;

public class ProdutoController : Controller
{
    
    private readonly ApplicationDbContext _context;

    public ProdutoController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var produtos = _context.Produtos.ToList();
        return View(produtos);
    }

    public IActionResult Create()
    {
        // Cria a instancia do ProdutoRegister
        var model = new ProdutoRegister();

      
        model.Categorias = LoadDropdownlistCategorias();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Create(Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        } else
        {
            
            // Cria a instancia do objeto ProdutoRegister
            var model = new ProdutoRegister();

          

            // Converte e armazena na propriedade Categorias a lista de SelectListItem com os dados das categorias que serão utilizados no dropdown
            model.Categorias = LoadDropdownlistCategorias();

            // Preenche a propridade Produto da model com o parametro
            model.Produto = produto;

            return View(model);
        }
     
    }

    public IActionResult Edit(int id)
    {
        // Busca o produto do banco pelo ID
        var produto = _context.Produtos.Find(id);
        // Verifica se o produto existe, caso não exista retorna um NotFound
        if (produto == null)
            return NotFound();
        
        // Cria a intancia do objeto ProdutoRegister
        var model = new ProdutoRegister();
        
        // Define o valor da propriedade do produto da model com o produto retornado da pesquisa
        model.Produto = produto;

        // Define o valor da propriedade Categorias da model com o método LoadDropdownlistCategorias
        model.Categorias = LoadDropdownlistCategorias();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
        public IActionResult Edit(Produto produto)
    {
       if(ModelState.IsValid)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            // Cria a intancia do objeto ProdutoRegister
            var model = new ProdutoRegister();

             // Define o valor da propriedade Categorias da model com o método LoadDropdownlistCategorias
            model.Categorias = LoadDropdownlistCategorias();
            
            // Define o valor da propriedade do produto da model com o produto retornado da pesquisa
            model.Produto = produto;

       
            return View(model);
        }
    }

    public IActionResult Delete(int id)
    {
        var produto = _context.Produtos.Find(id);

        if (produto == null)
            return NotFound();

       return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Produto produto)
    {
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    private List<SelectListItem> LoadDropdownlistCategorias()
    {
        // Busca no banco de dados todas as categorias
        var categorias = _context.Categorias.ToList();

        // Converte e armazena na propriedade Categorias a lista de SelectListItem com os dados das categorias que serão utilizados no dropdown
        var listaCategoriasParaDropdown = categorias.Select(x => new SelectListItem { Value = x.CategoriaId.ToString(), Text = x.Nome }).ToList();
        return listaCategoriasParaDropdown;
    }
}
