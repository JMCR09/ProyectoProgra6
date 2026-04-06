using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Generos;

public class IndexModel : PageModel
{
    private readonly GeneroService _generoService;

    public IndexModel(GeneroService generoService)
    {
        _generoService = generoService;
    }

    public List<Genero> Generos { get; set; } = [];

    public async Task OnGetAsync()
    {
        Generos = await _generoService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _generoService.DeleteAsync(id);
        return RedirectToPage();
    }
}
