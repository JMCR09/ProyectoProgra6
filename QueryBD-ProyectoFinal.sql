-- Creación de la BD
CREATE DATABASE Biblioteca
GO

USE Biblioteca
GO

-- Tabla de Autores
CREATE TABLE Autores (
    IdAutor INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(50),
    Apellidos VARCHAR(100),
    FechaNacimiento DATETIME,
    Nacionalidad VARCHAR(50)
)

-- Tabla de Géneros Literarios
CREATE TABLE Generos (
    IdGenero INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100)
)

-- Tabla de Libros
CREATE TABLE Libros (
    IdLibro INT PRIMARY KEY IDENTITY,
    Titulo VARCHAR(200),
    IdAutor INT,
    NumeroPaginas INT,
    IdGenero INT,
    FechaPublicacion DATETIME,
    FOREIGN KEY (IdAutor) REFERENCES Autores(IdAutor),
    FOREIGN KEY (IdGenero) REFERENCES Generos(IdGenero)
)

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY,
    Cedula VARCHAR(20),
    Nombre VARCHAR(50),
    Apellidos VARCHAR(100),
    Telefono VARCHAR(20),
    Direccion VARCHAR(200),
    FechaNacimiento DATETIME
)

-- Tabla de Préstamos
CREATE TABLE Prestamos (
    IdPrestamo INT PRIMARY KEY IDENTITY,
    IdUsuario INT,
    IdLibro INT,
    FechaPrestamo DATETIME,
    FechaDevolucion DATETIME,
    Estado VARCHAR(20),
    Comentarios VARCHAR(MAX),
    FechaDevolucionEfectiva DATETIME,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdLibro) REFERENCES Libros(IdLibro)
)
