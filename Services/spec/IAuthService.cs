using System;
using ComprasVentas.Dto.auth;

namespace ComprasVentas.Services.spec;

public interface IAuthService
{
    Task<AuthResponseDto> AuthenticateAsync(AuthRequestDto authRequestDto);

    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
}
