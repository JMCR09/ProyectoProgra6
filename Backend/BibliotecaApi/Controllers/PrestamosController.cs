using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PrestamosController : ControllerBase
{
    // Servicio para la logica de prestamos
    private readonly PrestamoService _prestamoService;

    // Logger para registrar errores del controlador
    private readonly ILogger<PrestamosController> _logger;

    public PrestamosController(PrestamoService prestamoService, ILogger<PrestamosController> logger)
    {
        _prestamoService = prestamoService;
        _logger = logger;
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los prestamos
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _prestamoService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los prestamos.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para obtener un prestamo por Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var prestamo = await _prestamoService.GetAsync(id);
            return prestamo is null ? NotFound() : Ok(prestamo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el prestamo con id {IdPrestamo}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para crear un nuevo prestamo
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Prestamo prestamo)
    {
        try
        {
            var prestamoCreado = await _prestamoService.AddAsync(prestamo);
            return Ok(prestamoCreado);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error de validacion al crear el prestamo.");
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el prestamo.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para actualizar un prestamo existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Prestamo prestamo)
    {
        try
        {
            var existingPrestamo = await _prestamoService.GetAsync(id);
            if (existingPrestamo is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            prestamo.IdPrestamo = id;
            await _prestamoService.UpdateAsync(prestamo);
            _logger.LogInformation("Actualizando el prestamo con ID: {IdPrestamo}", id);
            return Ok(prestamo);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error de validacion al actualizar el prestamo con id {IdPrestamo}.", id);
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el prestamo con id {IdPrestamo}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Metodo para eliminar un prestamo por Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eliminado = await _prestamoService.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el prestamo con id {IdPrestamo}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}
