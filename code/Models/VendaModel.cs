using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using code.Repository;

namespace code.Models;

public class VendaModel
{
    [Key]
    public int IdVenda { get; set; }    
    public int QtdVenda { get; set; }
    public float VlrUnitarioVenda { get; set; }
    public DateTime DthVenda { get; set; }
    public float VlrTotalVenda => QtdVenda * VlrUnitarioVenda;

    [ForeignKey("IdCliente")]
    public int IdCliente { get; set; }

    [ForeignKey("IdProduto")]
    public int IdProduto { get; set; }
    public virtual ClienteModel Cliente { get; set; }
    public virtual ProdutoModel Produto { get; set; }
}
