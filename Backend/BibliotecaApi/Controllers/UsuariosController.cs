using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
    // Servicio para la logica de usuarios
    private readonly UsuarioService _usuarioService;

    // Logger para registrar errores del controlador
    private readonly ILogger<UsuariosController> _logger;

    public UsuariosController(UsuarioService usuarioService, ILogger<UsuariosController> logger)
    {
        _usuarioService = usuarioService;
        _logger = logger;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los usuarios
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _usuarioService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los usuarios.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para obtener un usuario por Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var usuario = await _usuarioService.GetAsync(id);
            return usuario is null ? NotFound() : Ok(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el usuario con id {IdUsuario}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para crear un nuevo usuario
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Usuario usuario)
    {
        try
        {
            var usuarioCreado = await _usuarioService.AddAsync(usuario);
            return Ok(usuarioCreado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el usuario.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para actualizar un usuario existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
    {
        try
        {
            var existingUsuario = await _usuarioService.GetAsync(id);
            if (existingUsuario is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usuario.IdUsuario = id;
            await _usuarioService.UpdateAsync(usuario);
            _logger.LogInformation("Actualizando el usuario con ID: {IdUsuario}", id);
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el usuario con id {IdUsuario}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para eliminar un usuario por Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eliminado = await _usuarioService.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el usuario con id {IdUsuario}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}
