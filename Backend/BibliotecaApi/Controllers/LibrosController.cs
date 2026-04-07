using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LibrosController : ControllerBase
{
    // Servicio para la logica de libros
    private readonly LibroService _libroService;

    // Logger para registrar errores del controlador
    private readonly ILogger<LibrosController> _logger;

    public LibrosController(LibroService libroService, ILogger<LibrosController> logger)
    {
        _libroService = libroService;
        _logger = logger;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los libros
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _libroService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los libros.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para obtener un libro por Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var libro = await _libroService.GetAsync(id);
            return libro is null ? NotFound() : Ok(libro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el libro con id {IdLibro}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para crear un nuevo libro
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Libro libro)
    {
        try
        {
            var libroCreado = await _libroService.AddAsync(libro);
            return Ok(libroCreado);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error de validacion al crear el libro.");
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el libro.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para actualizar un libro existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Libro libro)
    {
        try
        {
            var existingLibro = await _libroService.GetAsync(id);
            if (existingLibro is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            libro.IdLibro = id;
            await _libroService.UpdateAsync(libro);
            _logger.LogInformation("Actualizando el libro con ID: {IdLibro}", id);
            return Ok(libro);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error de validacion al actualizar el libro con id {IdLibro}.", id);
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el libro con id {IdLibro}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para eliminar un libro por Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eliminado = await _libroService.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el libro con id {IdLibro}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}
