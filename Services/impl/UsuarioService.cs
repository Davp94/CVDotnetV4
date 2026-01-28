using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class UsuarioService(UsuarioRepository usuarioRepository) : IUsuarioService
{
    private  readonly UsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<List<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Select(u=>MapToDto(u)).ToList();
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    {
        var usuario =  await _usuarioRepository.GetUsuarioByIdAsync(id);
        if (usuario == null) return null;
        return MapToDto(usuario);
    }
    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Correo = dto.Correo,
            //TODO add password hashing
            Password = dto.Password,
            Persona = new Persona
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                FechaNacimiento = DateTime.ParseExact(dto.FechaNacimiento, "dd/MM/yyyy", null),
                Genero = dto.Genero,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Nacionalidad = dto.Nacionalidad
            }
        };

        await _usuarioRepository.CreateAsync(usuario);

        return MapToDto(usuario);
    }

    public async Task UpdateAsync(int id, CreateUsuarioDto dto)
    {
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
        if (usuario == null) throw new Exception("Usuario no encontrado");
        usuario.Nombre = dto.Nombre;
        usuario.Correo = dto.Correo;
        usuario.Password = dto.Password;
        //actualizar datos persona
        if (usuario.Persona != null)
        {
            usuario.Persona.Nombres = dto.Nombres;
            usuario.Persona.Apellidos = dto.Apellidos;
            usuario.Persona.FechaNacimiento = DateTime.ParseExact(dto.FechaNacimiento, "dd/MM/yyyy", null);
            usuario.Persona.Genero = dto.Genero;
            usuario.Persona.Telefono = dto.Telefono;
            usuario.Persona.Direccion = dto.Direccion;
            usuario.Persona.Nacionalidad = dto.Nacionalidad;
        }
        await _usuarioRepository.UpdateAsync(usuario);
         
    }

    public async Task DeleteAsync(int id)
    {
        await _usuarioRepository.DeleteAsync(id);
    }

    private UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Nombres = usuario.Persona?.Nombres ?? string.Empty,
            Apellidos = usuario.Persona?.Apellidos ?? string.Empty,
            FechaNacimiento = usuario.Persona?.FechaNacimiento.ToString("dd/MM/yyyy") ?? string.Empty,
            Genero = usuario.Persona?.Genero,
            Telefono = usuario.Persona?.Telefono,
            Direccion = usuario.Persona?.Direccion,
            Nacionalidad = usuario.Persona?.Nacionalidad
        };
    }
}
