using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Services;

public class PrestamoService
{
    // Contexto para acceder a la base de datos
    private readonly ApplicationDbContext _context;

    // Lista de estados permitidos para un prestamo
    private static readonly string[] EstadosValidos = ["Pendiente", "Devuelto", "Cancelado"];

    public PrestamoService(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los prestamos con sus relaciones
    public async Task<List<Prestamo>> GetAllAsync()
    {
        // Consulta LINQ con Include para cargar usuario, libro y autor del libro
        return await _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .ThenInclude(l => l!.Autor)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    // Metodo para obtener un prestamo por Id
    public async Task<Prestamo?> GetAsync(int id)
    {
        // Consulta LINQ para traer un prestamo puntual con sus relaciones
        return await _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .ThenInclude(l => l!.Autor)
            .FirstOrDefaultAsync(p => p.IdPrestamo == id);
    }

    // Metodo para crear un nuevo prestamo
    public async Task<Prestamo> AddAsync(Prestamo prestamo)
    {
        // Validaciones de negocio antes de guardar el prestamo
        await ValidarRelacionesAsync(prestamo);
        ValidarFechas(prestamo);

        // Valores por defecto al momento de crear
        prestamo.IdPrestamo = 0;
        prestamo.Estado = "Pendiente";
        prestamo.FechaDevolucionEfectiva = null;
        prestamo.Comentarios ??= string.Empty;

        _context.Prestamos.Add(prestamo);
        await _context.SaveChangesAsync();
        return prestamo;
    }

    // Metodo para actualizar un prestamo existente
    public async Task UpdateAsync(Prestamo prestamo)
    {
        // Validaciones de negocio antes de actualizar
        await ValidarRelacionesAsync(prestamo);
        ValidarFechas(prestamo);
        ValidarEstado(prestamo.Estado);

        var existingPrestamo = await _context.Prestamos.FindAsync(prestamo.IdPrestamo);
        if (existingPrestamo is null)
        {
            return;
        }

        existingPrestamo.IdUsuario = prestamo.IdUsuario;
        existingPrestamo.IdLibro = prestamo.IdLibro;
        existingPrestamo.FechaPrestamo = prestamo.FechaPrestamo;
        existingPrestamo.FechaDevolucion = prestamo.FechaDevolucion;
        existingPrestamo.Estado = prestamo.Estado;
        existingPrestamo.Comentarios = prestamo.Comentarios;
        existingPrestamo.FechaDevolucionEfectiva = prestamo.FechaDevolucionEfectiva;

        await _context.SaveChangesAsync();
    }

    // Metodo para eliminar un prestamo por Id
    public async Task<bool> DeleteAsync(int id)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);
        if (prestamo is null)
        {
            return false;
        }

        _context.Prestamos.Remove(prestamo);
        await _context.SaveChangesAsync();
        return true;
    }

    // Metodo para validar que el usuario y el libro existan
    private async Task ValidarRelacionesAsync(Prestamo prestamo)
    {
        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.IdUsuario == prestamo.IdUsuario);
        if (!usuarioExiste)
        {
            throw new InvalidOperationException("El usuario seleccionado no existe.");
        }

        var libroExiste = await _context.Libros.AnyAsync(l => l.IdLibro == prestamo.IdLibro);
        if (!libroExiste)
        {
            throw new InvalidOperationException("El libro seleccionado no existe.");
        }
    }

    // Metodo para validar las reglas de fechas del prestamo
    private static void ValidarFechas(Prestamo prestamo)
    {
        if (!prestamo.FechaPrestamo.HasValue || !prestamo.FechaDevolucion.HasValue)
        {
            throw new InvalidOperationException("Las fechas del prestamo son obligatorias.");
        }

        if (prestamo.FechaDevolucion < prestamo.FechaPrestamo)
        {
            throw new InvalidOperationException("La fecha de devolucion no puede ser menor a la fecha de prestamo.");
        }

        var dias = (prestamo.FechaDevolucion.Value.Date - prestamo.FechaPrestamo.Value.Date).TotalDays;
        if (dias > 30)
        {
            throw new InvalidOperationException("La duracion maxima del prestamo es de 30 dias.");
        }

        if (prestamo.FechaDevolucionEfectiva.HasValue && prestamo.FechaDevolucionEfectiva < prestamo.FechaPrestamo)
        {
            throw new InvalidOperationException("La fecha de devolucion efectiva no puede ser menor a la fecha de prestamo.");
        }
    }

    // Metodo para validar que el estado sea permitido
    private static void ValidarEstado(string? estado)
    {
        if (string.IsNullOrWhiteSpace(estado) || !EstadosValidos.Contains(estado))
        {
            throw new InvalidOperationException("El estado del prestamo no es valido.");
        }
    }
}
