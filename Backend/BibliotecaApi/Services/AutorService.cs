using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services;

public class AutorService
{
    // Contexto para acceder a la base de datos
    private readonly ApplicationDbContext _context;

    public AutorService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los autores ordenados por nombre y apellido
    public async Task<List<Autor>> GetAllAsync()
    {
        // Consulta LINQ para listar y ordenar autores
        return await _context.Autores.OrderBy(a => a.Nombre).ThenBy(a => a.Apellidos).ToListAsync();
    }

    // Metodo para obtener un autor por Id
    public async Task<Autor?> GetAsync(int id)
    {
        return await _context.Autores.FirstOrDefaultAsync(a => a.IdAutor == id);
    }

    // Metodo para crear un nuevo autor
    public async Task<Autor> AddAsync(Autor autor)
    {
        autor.IdAutor = 0;
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
        return autor;
    }

    // Metodo para actualizar un autor existente
    public async Task UpdateAsync(Autor autor)
    {
        var existingAutor = await _context.Autores.FindAsync(autor.IdAutor);
        if (existingAutor is null)
        {
            return;
        }

        existingAutor.Nombre = autor.Nombre;
        existingAutor.Apellidos = autor.Apellidos;
        existingAutor.FechaNacimiento = autor.FechaNacimiento;
        existingAutor.Nacionalidad = autor.Nacionalidad;

        await _context.SaveChangesAsync();
    }

    // Metodo para eliminar un autor por Id
    public async Task<bool> DeleteAsync(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor is null)
        {
            return false;
        }

        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();
        return true;
    }
    // Método para eliminar múltiples autores
    public async Task<bool> DeleteMultipleAsync(List<int> ids)
    {
        // Buscar todos los autores cuyos Id estén en la lista
        var autores = await _context.Autores
                                    .Where(a => ids.Contains(a.IdAutor))
                                    .ToListAsync();

        if (!autores.Any())
        {
            return false; // No se encontró ninguno
        }

        // Eliminar en lote
        _context.Autores.RemoveRange(autores);
        await _context.SaveChangesAsync();
        return true;
    }

}
