using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Prestamos;

public class CreateModel : PageModel
{
    // Servicios necesarios para cargar el formulario de prestamos
    private readonly PrestamoService _prestamoService;
    private readonly UsuarioService _usuarioService;
    private readonly LibroService _libroService;

    public CreateModel(PrestamoService prestamoService, UsuarioService usuarioService, LibroService libroService)
    {
        _prestamoService = prestamoService;
        _usuarioService = usuarioService;
        _libroService = libroService;
    }

    [BindProperty]
    public Prestamo Prestamo { get; set; } = new();

    // Catalogos para llenar los combos de usuario y libro
    public List<Usuario> Usuarios { get; set; } = [];
    public List<Libro> Libros { get; set; } = [];

    // Rango permitido para la fecha de devolucion segun la fecha de prestamo
    public string MinFechaDevolucion { get; set; } = string.Empty;
    public string MaxFechaDevolucion { get; set; } = string.Empty;

    // Metodo que carga catalogos y el prestamo cuando se edita
    public async Task OnGetAsync(int? id)
    {
        await CargarCatalogosAsync();

        if (id.HasValue && id.Value != 0)
        {
            Prestamo = await _prestamoService.GetAsync(id.Value) ?? new Prestamo();
        }
        else
        {
            Prestamo.FechaPrestamo = DateTime.Today;
            Prestamo.Estado = "Pendiente";
        }

        ConfigurarRangoFechas();
    }

    // Metodo que decide si crea o actualiza un prestamo
    public async Task<IActionResult> OnPostAsync()
    {
        if (Prestamo.IdPrestamo == 0)
        {
            await _prestamoService.AddAsync(Prestamo);
        }
        else
        {
            await _prestamoService.UpdateAsync(Prestamo);
        }

        return RedirectToPage("/Prestamos/Index");
    }

    // Metodo para cargar catalogos desde la API
    private async Task CargarCatalogosAsync()
    {
        Usuarios = await _usuarioService.GetAllAsync();
        Libros = await _libroService.GetAllAsync();
    }

    // Metodo para configurar el rango permitido de la fecha de devolucion
    private void ConfigurarRangoFechas()
    {
        var fechaBase = Prestamo.FechaPrestamo?.Date ?? DateTime.Today;
        MinFechaDevolucion = fechaBase.ToString("yyyy-MM-dd");
        MaxFechaDevolucion = fechaBase.AddDays(30).ToString("yyyy-MM-dd");
    }
}
