using System;
using ComprasVentas.Data;
using ComprasVentas.Dto.auth;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _dbContext;
    public Task<AuthResponseDto> AuthenticateAsync(AuthRequestDto authRequestDto)
    {
        //search user
        //crear tokens
        //return DTO
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        //search user
        //crear tokens
        //return DTO
        throw new NotImplementedException();
    }
}
