using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Autores;

public class IndexModel : PageModel
{
    // Servicio para consumir la API de autores
    private readonly AutorService _autorService;

    public IndexModel(AutorService autorService)
    {
        _autorService = autorService;
    }

    // Lista que se muestra en la tabla de autores
    public List<Autor> Autores { get; set; } = [];

    // Metodo que carga todos los autores al entrar a la pagina
    public async Task OnGetAsync()
    {
        Autores = await _autorService.GetAllAsync();
    }
    // Eliminar múltiple
    public async Task<IActionResult> OnPostDeleteMultipleAsync(string selectedIds)
    {
        if (string.IsNullOrEmpty(selectedIds))
            return RedirectToPage();

        var ids = selectedIds.Split(',').Select(int.Parse).ToList();
        await _autorService.DeleteMultipleAsync(ids);

        return RedirectToPage();
    }
    // Metodo para eliminar un autor desde la tabla
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _autorService.DeleteAsync(id);
        return RedirectToPage();
    }
}
