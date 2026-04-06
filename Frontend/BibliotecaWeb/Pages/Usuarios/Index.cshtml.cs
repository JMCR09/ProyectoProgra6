using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Usuarios;

public class IndexModel : PageModel
{
    private readonly UsuarioService _usuarioService;

    public IndexModel(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public List<Usuario> Usuarios { get; set; } = [];

    public async Task OnGetAsync()
    {
        Usuarios = await _usuarioService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _usuarioService.DeleteAsync(id);
        return RedirectToPage();
    }
}
