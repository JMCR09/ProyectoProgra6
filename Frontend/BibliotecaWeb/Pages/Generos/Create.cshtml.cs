using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Generos;

public class CreateModel : PageModel
{
    private readonly GeneroService _generoService;

    public CreateModel(GeneroService generoService)
    {
        _generoService = generoService;
    }

    [BindProperty]
    public Genero Genero { get; set; } = new();

    public async Task OnGetAsync(int? id)
    {
        if (id.HasValue && id.Value != 0)
        {
            Genero = await _generoService.GetAsync(id.Value) ?? new Genero();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Genero.IdGenero == 0)
        {
            await _generoService.AddAsync(Genero);
        }
        else
        {
            await _generoService.UpdateAsync(Genero);
        }

        return RedirectToPage("/Generos/Index");
    }
}
