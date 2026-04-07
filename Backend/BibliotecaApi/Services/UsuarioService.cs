
/*
 * Creado por: Jose Miguel Salas Chacon
 * Usuarios.cs - Modelo de datos para la entidad Usuario
 * 
 */



using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services;

public class UsuarioService
{
    // Contexto para acceder a la base de datos
    private readonly ApplicationDbContext _context;

    public UsuarioService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los usuarios ordenados por nombre y apellido
    public async Task<List<Usuario>> GetAllAsync()
    {
        // Consulta LINQ para listar usuarios ordenados
        return await _context.Usuarios.OrderBy(u => u.Nombre).ThenBy(u => u.Apellidos).ToListAsync();
    }

    // Metodo para obtener un usuario por Id
    public async Task<Usuario?> GetAsync(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);
    }

    // Metodo para crear un nuevo usuario
    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        usuario.IdUsuario = 0;
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    // Metodo para actualizar un usuario existente
    public async Task UpdateAsync(Usuario usuario)
    {
        var existingUsuario = await _context.Usuarios.FindAsync(usuario.IdUsuario);
        if (existingUsuario is null)
        {
            return;
        }

        existingUsuario.Cedula = usuario.Cedula;
        existingUsuario.Nombre = usuario.Nombre;
        existingUsuario.Apellidos = usuario.Apellidos;
        existingUsuario.Telefono = usuario.Telefono;
        existingUsuario.Direccion = usuario.Direccion;
        existingUsuario.FechaNacimiento = usuario.FechaNacimiento;

        await _context.SaveChangesAsync();
    }

    // Metodo para eliminar un usuario por Id
    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            return false;
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}
