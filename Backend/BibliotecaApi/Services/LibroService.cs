using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services;

public class LibroService
{
    // Contexto para acceder a la base de datos
    private readonly ApplicationDbContext _context;

    public LibroService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los libros con su autor y genero
    public async Task<List<Libro>> GetAllAsync()
    {
        // Consulta LINQ con Include para cargar relaciones y ordenar por titulo
        return await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Genero)
            .OrderBy(l => l.Titulo)
            .ToListAsync();
    }

    // Metodo para obtener un libro por Id incluyendo sus relaciones
    public async Task<Libro?> GetAsync(int id)
    {
        // Consulta LINQ para traer un solo libro con su autor y genero
        return await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Genero)
            .FirstOrDefaultAsync(l => l.IdLibro == id);
    }

    // Metodo para crear un nuevo libro
    public async Task<Libro> AddAsync(Libro libro)
    {
        await ValidarRelacionesAsync(libro);

        libro.IdLibro = 0;
        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();
        return libro;
    }

    // Metodo para actualizar un libro existente
    public async Task UpdateAsync(Libro libro)
    {
        await ValidarRelacionesAsync(libro);

        var existingLibro = await _context.Libros.FindAsync(libro.IdLibro);
        if (existingLibro is null)
        {
            return;
        }

        existingLibro.Titulo = libro.Titulo;
        existingLibro.IdAutor = libro.IdAutor;
        existingLibro.NumeroPaginas = libro.NumeroPaginas;
        existingLibro.IdGenero = libro.IdGenero;
        existingLibro.FechaPublicacion = libro.FechaPublicacion;

        await _context.SaveChangesAsync();
    }

    // Metodo para eliminar un libro por Id
    public async Task<bool> DeleteAsync(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro is null)
        {
            return false;
        }

        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync();
        return true;
    }

    // Metodo para validar que el autor y el genero existan antes de guardar
    private async Task ValidarRelacionesAsync(Libro libro)
    {
        var autorExiste = await _context.Autores.AnyAsync(a => a.IdAutor == libro.IdAutor);
        if (!autorExiste)
        {
            throw new InvalidOperationException("El autor seleccionado no existe.");
        }

        var generoExiste = await _context.Generos.AnyAsync(g => g.IdGenero == libro.IdGenero);
        if (!generoExiste)
        {
            throw new InvalidOperationException("El genero seleccionado no existe.");
        }
    }
}
