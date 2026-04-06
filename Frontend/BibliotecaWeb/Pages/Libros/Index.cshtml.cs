using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Libros;

public class IndexModel : PageModel
{
    private readonly LibroService _libroService;

    public IndexModel(LibroService libroService)
    {
        _libroService = libroService;
    }

    public List<Libro> Libros { get; set; } = [];

    public async Task OnGetAsync()
    {
        Libros = await _libroService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _libroService.DeleteAsync(id);
        return RedirectToPage();
    }
}
