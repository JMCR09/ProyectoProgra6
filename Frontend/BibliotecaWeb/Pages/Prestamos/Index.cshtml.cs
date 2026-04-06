using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Prestamos;

public class IndexModel : PageModel
{
    // Servicio para consumir la API de prestamos
    private readonly PrestamoService _prestamoService;

    public IndexModel(PrestamoService prestamoService)
    {
        _prestamoService = prestamoService;
    }

    // Filtro opcional por titulo del libro
    [BindProperty(SupportsGet = true)]
    public string? FiltroLibro { get; set; }

    // Filtro opcional por nombre del usuario
    [BindProperty(SupportsGet = true)]
    public string? FiltroUsuario { get; set; }

    // Filtro opcional por fecha
    [BindProperty(SupportsGet = true)]
    public DateTime? FiltroFecha { get; set; }

    // Lista que se muestra en la tabla de prestamos
    public List<Prestamo> Prestamos { get; set; } = [];

    // Metodo que carga y filtra la lista de prestamos
    public async Task OnGetAsync()
    {
        Prestamos = await _prestamoService.GetAllAsync();

        // Filtro por nombre del libro
        if (!string.IsNullOrWhiteSpace(FiltroLibro))
        {
            Prestamos = Prestamos
                .Where(p => (p.Libro?.Titulo ?? string.Empty)
                    .Contains(FiltroLibro, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Filtro por nombre completo del usuario
        if (!string.IsNullOrWhiteSpace(FiltroUsuario))
        {
            Prestamos = Prestamos
                .Where(p => ($"{p.Usuario?.Nombre} {p.Usuario?.Apellidos}")
                    .Contains(FiltroUsuario, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Filtro por fecha de prestamo o fecha de devolucion
        if (FiltroFecha.HasValue)
        {
            var fecha = FiltroFecha.Value.Date;
            Prestamos = Prestamos
                .Where(p => p.FechaPrestamo?.Date == fecha || p.FechaDevolucion?.Date == fecha)
                .ToList();
        }
    }

    // Metodo para eliminar un prestamo desde la tabla
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _prestamoService.DeleteAsync(id);
        return RedirectToPage();
    }
}
