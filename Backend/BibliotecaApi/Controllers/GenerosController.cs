using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GenerosController : ControllerBase
{
    // Servicio para la logica de generos literarios
    private readonly GeneroService _generoService;

    // Logger para registrar errores del controlador
    private readonly ILogger<GenerosController> _logger;

    public GenerosController(GeneroService generoService, ILogger<GenerosController> logger)
    {
        _generoService = generoService;
        _logger = logger;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los generos
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _generoService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los generos.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para obtener un genero por Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var genero = await _generoService.GetAsync(id);
            return genero is null ? NotFound() : Ok(genero);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el genero con id {IdGenero}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para crear un nuevo genero
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Genero genero)
    {
        try
        {
            var generoCreado = await _generoService.AddAsync(genero);
            return Ok(generoCreado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el genero.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para actualizar un genero existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Genero genero)
    {
        try
        {
            var existingGenero = await _generoService.GetAsync(id);
            if (existingGenero is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            genero.IdGenero = id;
            await _generoService.UpdateAsync(genero);
            _logger.LogInformation("Actualizando el genero con ID: {IdGenero}", id);
            return Ok(genero);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el genero con id {IdGenero}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para eliminar un genero por Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eliminado = await _generoService.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el genero con id {IdGenero}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}
