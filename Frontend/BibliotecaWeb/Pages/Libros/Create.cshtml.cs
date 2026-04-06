using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Libros;

public class CreateModel : PageModel
{
    // Servicios necesarios para cargar el formulario y sus catalogos
    private readonly LibroService _libroService;
    private readonly AutorService _autorService;
    private readonly GeneroService _generoService;

    public CreateModel(LibroService libroService, AutorService autorService, GeneroService generoService)
    {
        _libroService = libroService;
        _autorService = autorService;
        _generoService = generoService;
    }

    [BindProperty]
    public Libro Libro { get; set; } = new();

    // Catalogos para llenar los combos de autor y genero
    public List<Autor> Autores { get; set; } = [];
    public List<Genero> Generos { get; set; } = [];

    // Metodo que carga catalogos y el libro cuando se edita
    public async Task OnGetAsync(int? id)
    {
        await CargarCatalogosAsync();

        if (id.HasValue && id.Value != 0)
        {
            Libro = await _libroService.GetAsync(id.Value) ?? new Libro();
        }
    }

    // Metodo que decide si crea o actualiza un libro
    public async Task<IActionResult> OnPostAsync()
    {
        if (Libro.IdLibro == 0)
        {
            await _libroService.AddAsync(Libro);
        }
        else
        {
            await _libroService.UpdateAsync(Libro);
        }

        return RedirectToPage("/Libros/Index");
    }

    // Metodo para cargar catalogos desde la API
    private async Task CargarCatalogosAsync()
    {
        Autores = await _autorService.GetAllAsync();
        Generos = await _generoService.GetAllAsync();
    }
}
