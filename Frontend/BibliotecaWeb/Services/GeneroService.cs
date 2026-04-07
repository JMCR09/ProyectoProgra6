using BibliotecaWeb.Models;
using System.Net.Http.Json;

namespace BibliotecaWeb.Services;

public class GeneroService
{
    // Cliente HTTP para consumir el backend
    private readonly HttpClient _httpClient;

    public GeneroService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los generos desde la API
    public async Task<List<Genero>> GetAllAsync()
        => await _httpClient.GetFromJsonAsync<List<Genero>>("/Generos") ?? [];

    // Metodo para obtener un genero por Id
    public async Task<Genero?> GetAsync(int id)
        => await _httpClient.GetFromJsonAsync<Genero>($"/Generos/{id}");

    // Metodo para crear un nuevo genero
    public async Task AddAsync(Genero genero)
    {
        var response = await _httpClient.PostAsJsonAsync("/Generos", genero);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para actualizar un genero existente
    public async Task UpdateAsync(Genero genero)
    {
        var response = await _httpClient.PutAsJsonAsync($"/Generos/{genero.IdGenero}", genero);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para eliminar un genero por Id
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/Generos/{id}");
        response.EnsureSuccessStatusCode();
    }
}
