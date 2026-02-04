using System;
using System.ComponentModel.DataAnnotations;

namespace ComprasVentas.Dto.auth;

public class AuthRequestDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
