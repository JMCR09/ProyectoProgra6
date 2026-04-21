using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Autores;

public class CreateModel : PageModel
{
    // Servicio para consumir la API de autores
    private readonly AutorService _autorService;

    public CreateModel(AutorService autorService)
    {
        _autorService = autorService;
    }

    // Objeto bindeado al formulario de autores
    [BindProperty]
    public Autor Autor { get; set; } = new();

    // Metodo que carga el autor si se esta editando
    public async Task OnGetAsync(int? id)
    {
        if (id.HasValue && id.Value != 0)
        {
            Autor = await _autorService.GetAsync(id.Value) ?? new Autor();
        }
    }

    // Metodo que decide si crea o actualiza segun el Id
    public async Task<IActionResult> OnPostAsync()
    {
        if (Autor.IdAutor == 0)
        {
            await _autorService.AddAsync(Autor);
        }
        else
        {
            await _autorService.UpdateAsync(Autor);
        }

        return RedirectToPage("/Autores/Index");
    }

}
