using System;
using System.ComponentModel.DataAnnotations;

namespace ComprasVentas.Dto.auth;

public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
