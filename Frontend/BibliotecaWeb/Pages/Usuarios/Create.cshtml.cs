using BibliotecaWeb.Models;
using BibliotecaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaWeb.Pages.Usuarios;

public class CreateModel : PageModel
{
    private readonly UsuarioService _usuarioService;

    public CreateModel(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [BindProperty]
    public Usuario Usuario { get; set; } = new();

    public async Task OnGetAsync(int? id)
    {
        if (id.HasValue && id.Value != 0)
        {
            Usuario = await _usuarioService.GetAsync(id.Value) ?? new Usuario();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Usuario.IdUsuario == 0)
        {
            await _usuarioService.AddAsync(Usuario);
        }
        else
        {
            await _usuarioService.UpdateAsync(Usuario);
        }

        return RedirectToPage("/Usuarios/Index");
    }
}
