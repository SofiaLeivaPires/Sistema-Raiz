using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Raiz.Models;

namespace Raiz.ViewModels;

public class ProdutoSearch
{
    #region Filtros

    [DisplayName("Id")]
    public int? ProdutoId { get; set; }

    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [DisplayName("Preço inicial")]
    public double? PrecoInicial { get; set; }

    [DisplayName("Preço final")]
    public double? PrecoFinal { get; set; }

    [DisplayName("Categoria")]
    public int? CategoriaId { get; set; }

    #endregion

    // Lista para alimentar o dropdownlist
    public List<SelectListItem> Categorias { get; set; }

    // Lista de produtos que é o resultado da pesquisa
    public IEnumerable<Produto> Resultado { get; set; }
}