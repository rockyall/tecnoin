using System.ComponentModel.DataAnnotations;

namespace prueba1.Models;

public class ClienteViewModel
{
    public string? Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; }
    
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Correo inválido")]
    public string Email { get; set; }

}