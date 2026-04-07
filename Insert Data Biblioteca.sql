USE Biblioteca;
GO

INSERT INTO Autores (Nombre, Apellidos, FechaNacimiento, Nacionalidad)
VALUES
    ('Gabriel', 'Garcia Marquez', '1927-03-06', 'Colombiana'),
    ('Isabel', 'Allende', '1942-08-02', 'Chilena'),
    ('Jorge Luis', 'Borges', '1899-08-24', 'Argentina'),
    ('Julio', 'Cortazar', '1914-08-26', 'Argentina'),
    ('Laura', 'Esquivel', '1950-09-30', 'Mexicana'),
    ('Mario', 'Benedetti', '1920-09-14', 'Uruguaya');
GO

INSERT INTO Generos (Nombre)
VALUES
    ('Realismo magico'),
    ('Novela historica'),
    ('Cuento'),
    ('Ficcion'),
    ('Romance'),
    ('Ensayo');
GO

INSERT INTO Usuarios (Cedula, Nombre, Apellidos, Telefono, Direccion, FechaNacimiento)
VALUES
    ('1-1111-1111', 'Daniel', 'Jimenez Soto', '8888-1111', 'Cartago, Costa Rica', '2001-05-12'),
    ('2-2222-2222', 'Melissa', 'Rodriguez Mora', '8888-2222', 'San Jose, Costa Rica', '1999-11-03'),
    ('3-3333-3333', 'Andres', 'Vargas Solano', '8888-3333', 'Heredia, Costa Rica', '2002-02-18'),
    ('4-4444-4444', 'Paola', 'Fernandez Ruiz', '8888-4444', 'Alajuela, Costa Rica', '2000-07-24'),
    ('5-5555-5555', 'Steven', 'Ulate Castro', '8888-5555', 'Puntarenas, Costa Rica', '1998-09-09'),
    ('6-6666-6666', 'Valeria', 'Campos Araya', '8888-6666', 'Limon, Costa Rica', '2003-01-15');
GO

INSERT INTO Libros (Titulo, IdAutor, NumeroPaginas, IdGenero, FechaPublicacion)
VALUES
    ('Cien anos de soledad', 1, 471, 1, '1967-05-30'),
    ('El amor en los tiempos del colera', 1, 348, 5, '1985-09-05'),
    ('La casa de los espiritus', 2, 433, 2, '1982-01-01'),
    ('Eva Luna', 2, 304, 4, '1987-01-01'),
    ('Ficciones', 3, 174, 3, '1944-01-01'),
    ('Rayuela', 4, 736, 4, '1963-06-28'),
    ('Como agua para chocolate', 5, 246, 5, '1989-01-01'),
    ('La tregua', 6, 219, 6, '1960-01-01');
GO

INSERT INTO Prestamos (IdUsuario, IdLibro, FechaPrestamo, FechaDevolucion, Estado, Comentarios, FechaDevolucionEfectiva)
VALUES
    (1, 1, '2026-03-01', '2026-03-20', 'Pendiente', 'Prestamo activo para lectura del curso.', NULL),
    (2, 3, '2026-02-01', '2026-02-18', 'Devuelto', 'Devuelto en excelente estado.', '2026-02-17'),
    (3, 5, '2026-01-10', '2026-01-25', 'Cancelado', 'El usuario cancelo el prestamo antes de retirarlo.', NULL),
    (4, 6, '2026-03-05', '2026-03-25', 'Pendiente', 'Pendiente de entrega a fin de mes.', NULL);
GO
