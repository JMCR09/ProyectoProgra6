using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Models;

// Contexto principal de Entity Framework para la base Biblioteca
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Autor> Autores => Set<Autor>();
    public DbSet<Genero> Generos => Set<Genero>();
    public DbSet<Libro> Libros => Set<Libro>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();

    // Configura tablas, tipos de datos y relaciones entre entidades
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.ToTable("Autores");
            entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Apellidos).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Nacionalidad).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.ToTable("Generos");
            entity.Property(e => e.Nombre).HasMaxLength(100).IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.ToTable("Libros");
            entity.Property(e => e.Titulo).HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");

            // Relacion entre libros y autores
            entity.HasOne(d => d.Autor)
                .WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdAutor);

            // Relacion entre libros y generos
            entity.HasOne(d => d.Genero)
                .WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdGenero);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.Property(e => e.Cedula).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Apellidos).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Telefono).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.Direccion).HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.ToTable("Prestamos");
            entity.Property(e => e.Estado).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.Comentarios).IsUnicode(false);
            entity.Property(e => e.FechaPrestamo).HasColumnType("datetime");
            entity.Property(e => e.FechaDevolucion).HasColumnType("datetime");
            entity.Property(e => e.FechaDevolucionEfectiva).HasColumnType("datetime");

            // Relacion entre prestamos y usuarios
            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdUsuario);

            // Relacion entre prestamos y libros
            entity.HasOne(d => d.Libro)
                .WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdLibro);
        });
    }
}
