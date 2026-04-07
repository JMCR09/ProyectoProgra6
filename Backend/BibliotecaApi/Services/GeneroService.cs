using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services;

public class GeneroService
{
    // Contexto para acceder a la base de datos
    private readonly ApplicationDbContext _context;

    public GeneroService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los generos ordenados alfabeticamente
    public async Task<List<Genero>> GetAllAsync()
    {
        // Consulta LINQ para ordenar los generos por nombre
        return await _context.Generos.OrderBy(g => g.Nombre).ToListAsync();
    }

    // Metodo para obtener un genero por Id
    public async Task<Genero?> GetAsync(int id)
    {
        return await _context.Generos.FirstOrDefaultAsync(g => g.IdGenero == id);
    }

    // Metodo para crear un nuevo genero
    public async Task<Genero> AddAsync(Genero genero)
    {
        genero.IdGenero = 0;
        _context.Generos.Add(genero);
        await _context.SaveChangesAsync();
        return genero;
    }

    // Metodo para actualizar un genero existente
    public async Task UpdateAsync(Genero genero)
    {
        var existingGenero = await _context.Generos.FindAsync(genero.IdGenero);
        if (existingGenero is null)
        {
            return;
        }

        existingGenero.Nombre = genero.Nombre;
        await _context.SaveChangesAsync();
    }

    // Metodo para eliminar un genero por Id
    public async Task<bool> DeleteAsync(int id)
    {
        var genero = await _context.Generos.FindAsync(id);
        if (genero is null)
        {
            return false;
        }

        _context.Generos.Remove(genero);
        await _context.SaveChangesAsync();
        return true;
    }
}
