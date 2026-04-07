using BibliotecaWeb.Models;
using System.Net.Http.Json;

namespace BibliotecaWeb.Services;

public class PrestamoService
{
    // Cliente HTTP para consumir el backend
    private readonly HttpClient _httpClient;

    public PrestamoService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los prestamos desde la API
    public async Task<List<Prestamo>> GetAllAsync()
        => await _httpClient.GetFromJsonAsync<List<Prestamo>>("/Prestamos") ?? [];

    // Metodo para obtener un prestamo por Id
    public async Task<Prestamo?> GetAsync(int id)
        => await _httpClient.GetFromJsonAsync<Prestamo>($"/Prestamos/{id}");

    // Metodo para crear un nuevo prestamo
    public async Task AddAsync(Prestamo prestamo)
    {
        var response = await _httpClient.PostAsJsonAsync("/Prestamos", prestamo);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para actualizar un prestamo existente
    public async Task UpdateAsync(Prestamo prestamo)
    {
        var response = await _httpClient.PutAsJsonAsync($"/Prestamos/{prestamo.IdPrestamo}", prestamo);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para eliminar un prestamo por Id
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/Prestamos/{id}");
        response.EnsureSuccessStatusCode();
    }
}
