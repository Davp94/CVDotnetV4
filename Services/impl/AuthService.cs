using System;
using ComprasVentas.Common;
using ComprasVentas.Data;
using ComprasVentas.Dto.auth;
using ComprasVentas.Exceptions;
using ComprasVentas.Models;
using ComprasVentas.Services.spec;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ComprasVentas.Services.impl;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _dbContext;
    private readonly JwtSettings _jwtSettings;  

    public AuthService(AppDbContext context, ITokenService tokenService, IOptions<JwtSettings> jwtSettings)
    {
        _dbContext = context;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    } 
    public async Task<AuthResponseDto> AuthenticateAsync(AuthRequestDto authRequestDto)
    {
        //search user
        var user = await _dbContext.Usuarios
            .Include(r=>r.Roles)
            .ThenInclude(r=>r.Permisos)
            .FirstOrDefaultAsync(u => u.Nombre == authRequestDto.Username);
        if(user == null || !BCrypt.Net.BCrypt.Verify(authRequestDto.Password, user.Password))
        {
            throw new BadRequestException("Credenciales no validas");
        }
        //crear tokens
        var accessToken = _tokenService.GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
          Token = refreshToken,
          Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
          UsuarioId = user.Id,
          Created = DateTime.UtcNow  
        };
        _dbContext.RefreshTokens.Add(refreshTokenEntity);
        await _dbContext.SaveChangesAsync();
        //return DTO
        return new AuthResponseDto
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            identifier = user.Id
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        //search refreshToken
        var refreshTokenEntity = await _dbContext.RefreshTokens
            .Include(r => r.Usuario)
            .ThenInclude(u=>u.Roles)
            .ThenInclude(p=>p.Permisos)
            .SingleOrDefaultAsync(r=>r.Token == refreshTokenDto.RefreshToken);

        if(refreshTokenEntity == null || refreshTokenEntity.IsRevoked)
        {
            throw new BadRequestException("Refresh Token invalido");
        }
        //crear tokens
        var accessToken = _tokenService.GenerateToken(refreshTokenEntity.Usuario);
        var refreshToken = _tokenService.GenerateRefreshToken();

        refreshTokenEntity.Revoked = DateTime.UtcNow;
        refreshTokenEntity.IsRevoked = true;
        _dbContext.RefreshTokens.Update(refreshTokenEntity);

        var newRefreshTokenEntity = new RefreshToken
        {
          Token = refreshToken,
          Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
          UsuarioId = refreshTokenEntity.Id,
          Created = DateTime.UtcNow  
        };
        _dbContext.RefreshTokens.Add(refreshTokenEntity);
        await _dbContext.SaveChangesAsync(); 

        return new AuthResponseDto
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            identifier = refreshTokenEntity.Id
        };
    }
}
