using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Prestamos;

public class EstadoModel : PageModel
{
    // Servicio para consumir la API de prestamos
    private readonly PrestamoService _prestamoService;

    public EstadoModel(PrestamoService prestamoService)
    {
        _prestamoService = prestamoService;
    }

    [BindProperty]
    public Prestamo Prestamo { get; set; } = new();

    // Datos de apoyo para mostrar informacion del prestamo seleccionado
    public string NombreUsuario { get; set; } = string.Empty;
    public string TituloLibro { get; set; } = string.Empty;
    public string MinFechaDevolucionEfectiva { get; set; } = string.Empty;

    // Metodo que carga el prestamo seleccionado y prepara la vista
    public async Task<IActionResult> OnGetAsync(int id)
    {
        var prestamo = await _prestamoService.GetAsync(id);
        if (prestamo is null)
        {
            return RedirectToPage("/Prestamos/Index");
        }

        Prestamo = prestamo;
        ConfigurarVista();
        return Page();
    }

    // Metodo para actualizar solo el estado, comentarios y fecha efectiva
    public async Task<IActionResult> OnPostAsync()
    {
        if (Prestamo.Estado != "Devuelto")
        {
            Prestamo.FechaDevolucionEfectiva = null;
        }

        await _prestamoService.UpdateAsync(Prestamo);
        return RedirectToPage("/Prestamos/Index");
    }

    // Metodo para preparar la informacion visual de la pagina
    private void ConfigurarVista()
    {
        NombreUsuario = $"{Prestamo.Usuario?.Nombre} {Prestamo.Usuario?.Apellidos}".Trim();
        TituloLibro = Prestamo.Libro?.Titulo ?? string.Empty;
        MinFechaDevolucionEfectiva = (Prestamo.FechaPrestamo?.Date ?? DateTime.Today).ToString("yyyy-MM-dd");
    }
}
