using System;
using System.ComponentModel.DataAnnotations;

namespace ComprasVentas.Dto;

public class CreateUsuarioDto
{
    [Required(ErrorMessage = "El Nombre es obligatoria.")]
    [StringLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress]
    [StringLength(50)]
    public string Correo { get; set; } = string.Empty; //dsadsad@dsad.dsad

    [Required(ErrorMessage = "El password es obligatorio.")]
    [StringLength(16, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$", ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres, contener al menos una letra minúscula, una mayúscula, un número y un carácter especial.")]
    public string Password { get; set; } = string.Empty;

    //Datos de persona
    [Required]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/\d{4}$", ErrorMessage = "El formato de fecha debe ser dd/mm/yyyy.")]
    public string FechaNacimiento { get; set; }

    [StringLength(20)]
    public string? Genero { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Nacionalidad { get; set; }

    public List<int> RolIds { get; set; } = [];
}
