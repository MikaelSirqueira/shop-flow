using System.ComponentModel.DataAnnotations;

namespace code.Models;

public class ProdutoModel
{
    [Key]
    public int IdProduto { get; set; }

    [Required(ErrorMessage = "Digite a descrição do produto")]
    public string DscProduto { get; set; } = string.Empty;

    [Required(ErrorMessage = "Digite o valor do produto")]
    public float VlrUnitario { get; set; }
}
