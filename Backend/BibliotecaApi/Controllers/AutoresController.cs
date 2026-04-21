using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AutoresController : ControllerBase
{
    // Servicio para la logica de autores
    private readonly AutorService _autorService;

    // Logger para registrar errores del controlador
    private readonly ILogger<AutoresController> _logger;

    public AutoresController(AutorService autorService, ILogger<AutoresController> logger)
    {
        _autorService = autorService;
        _logger = logger;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los autores
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _autorService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los autores.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para obtener un autor por Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var autor = await _autorService.GetAsync(id);
            return autor is null ? NotFound() : Ok(autor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el autor con id {IdAutor}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para crear un nuevo autor
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Autor autor)
    {
        try
        {
            var autorCreado = await _autorService.AddAsync(autor);
            return Ok(autorCreado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el autor.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para actualizar un autor existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Autor autor)
    {
        try
        {
            var existingAutor = await _autorService.GetAsync(id);
            if (existingAutor is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            autor.IdAutor = id;
            await _autorService.UpdateAsync(autor);
            _logger.LogInformation("Actualizando el autor con ID: {IdAutor}", id);
            return Ok(autor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el autor con id {IdAutor}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para eliminar un autor por Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eliminado = await _autorService.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el autor con id {IdAutor}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    // Método para eliminar múltiples autores
    [HttpDelete]
    public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
    {
        try
        {
            if (ids == null || !ids.Any())
                return BadRequest("Debe proporcionar al menos un Id de autor.");

            await _autorService.DeleteMultipleAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar múltiples autores.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

}
