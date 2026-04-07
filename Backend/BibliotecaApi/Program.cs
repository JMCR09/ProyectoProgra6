using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Servicios principales de ASP.NET Core
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Evita errores de serializacion por referencias circulares entre entidades relacionadas
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuracion del DbContext para conectarse a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BibliotecaDb"),
        sqlServerOptions =>
        {
            sqlServerOptions.EnableRetryOnFailure();
        }));

// Registro de servicios de la aplicacion
builder.Services.AddScoped<AutorService>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<LibroService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PrestamoService>();

var app = builder.Build();

// Habilita Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuracion del pipeline HTTP
app.UseHttpsRedirection();

app.UseAuthorization();

// Mapea todos los controladores del proyecto
app.MapControllers();

app.Run();
