using BibliotecaWeb.Models;
using System.Net.Http.Json;

namespace BibliotecaWeb.Services;

public class AutorService
{
    // Cliente HTTP para consumir el backend
    private readonly HttpClient _httpClient;

    public AutorService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los autores desde la API
    public async Task<List<Autor>> GetAllAsync()
        => await _httpClient.GetFromJsonAsync<List<Autor>>("/Autores") ?? [];

    // Metodo para obtener un autor por Id
    public async Task<Autor?> GetAsync(int id)
        => await _httpClient.GetFromJsonAsync<Autor>($"/Autores/{id}");

    // Metodo para crear un nuevo autor
    public async Task AddAsync(Autor autor)
        => await _httpClient.PostAsJsonAsync("/Autores", autor);

    // Metodo para actualizar un autor existente
    public async Task UpdateAsync(Autor autor)
        => await _httpClient.PutAsJsonAsync($"/Autores/{autor.IdAutor}", autor);

    // Metodo para eliminar un autor por Id
    public async Task DeleteAsync(int id)
        => await _httpClient.DeleteAsync($"/Autores/{id}");
}
