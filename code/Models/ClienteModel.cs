using System.ComponentModel.DataAnnotations;

namespace code.Models;

public class ClienteModel
{
    [Key]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "Digite o nome do Cliente")]
    public string NmCliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Digite a cidade do Cliente")]
    public string Cidade { get; set; } = string.Empty;
}
