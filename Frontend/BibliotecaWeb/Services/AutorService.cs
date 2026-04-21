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
    {
        var response = await _httpClient.PostAsJsonAsync("/Autores", autor);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para actualizar un autor existente
    public async Task UpdateAsync(Autor autor)
    {
        var response = await _httpClient.PutAsJsonAsync($"/Autores/{autor.IdAutor}", autor);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para eliminar un autor por Id
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/Autores/{id}");
        response.EnsureSuccessStatusCode();
    }
    // Método para eliminar múltiples autores
    public async Task DeleteMultipleAsync(List<int> ids)
    {
        
        var request = new HttpRequestMessage(HttpMethod.Delete, "/Autores")
        {
            Content = JsonContent.Create(ids) 
        };

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

}
