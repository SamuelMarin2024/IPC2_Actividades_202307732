# Actividad 26 - Integración de Datos

**Curso:** Introducción a la Programación y Computación 2

**Tema:** Consumo de APIs Externas y Carga Masiva de Datos

**Nombre:** Samuel Ernesto Marín Higueros

**Carné:** 202307732

**Fecha:** 26 de junio de 2026

---

# Parte 1 - Evaluación Conceptual

## 1. Formatos de intercambio

| Formato | Ventajas | Desventajas |
|---------|----------|-------------|
| **CSV** | Es extremadamente ligero, fácil de generar desde Excel y muy rápido de procesar. | No soporta estructuras jerárquicas; únicamente almacena datos planos. |
| **XML** | Permite representar estructuras jerárquicas y diferentes tipos de datos. | Es más verboso y genera archivos más pesados que CSV o JSON. |

## 2. Serialización y Deserialización

La **serialización** consiste en convertir un objeto de C# en una representación JSON para almacenarlo o enviarlo mediante una API.

La **deserialización** realiza el proceso inverso, convirtiendo un documento JSON recibido desde una API en un objeto de C# utilizando la biblioteca `System.Text.Json`.

En aplicaciones REST ambos procesos permiten intercambiar información entre diferentes sistemas de forma sencilla y estandarizada.

## 3. Error de rendimiento N+1

El problema **N+1** ocurre cuando por cada registro leído desde un archivo masivo se realiza una consulta individual a una API o a la base de datos.

Este comportamiento incrementa considerablemente el tiempo de procesamiento debido a la gran cantidad de operaciones repetidas.

La solución consiste en aplicar una estrategia de **Batching**, agrupando los registros para procesarlos e insertarlos en un solo lote, reduciendo el número de accesos a la base de datos y mejorando significativamente el rendimiento.

---

# Parte 2 - Implementación Práctica

## Desafío 1 - Consumo de Endpoints y Deserialización

```csharp
using System.Net.Http;
using System.Text.Json;

public class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

private static readonly HttpClient client = new HttpClient();

public async Task<Alumno?> ObtenerAlumnoAsync()
{
    try
    {
        HttpResponseMessage response =
            await client.GetAsync("https://api.usac.edu/v1/alumnos");

        response.EnsureSuccessStatusCode();

        string json =
            await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        Alumno? alumno =
            JsonSerializer.Deserialize<Alumno>(json, options);

        return alumno;
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Error HTTP: {ex.Message}");
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"Error JSON: {ex.Message}");
    }

    return null;
}
```

## Desafío 2 - Endpoint para carga masiva CSV

```csharp
[HttpPost]
public async Task<IActionResult> Upload(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("Archivo inválido.");

    var alumnos = new List<Alumno>();

    using var stream = file.OpenReadStream();
    using var reader = new StreamReader(stream);

    while (!reader.EndOfStream)
    {
        string? linea = await reader.ReadLineAsync();

        if (string.IsNullOrWhiteSpace(linea))
            continue;

        var columnas = linea.Split(',');

        alumnos.Add(new Alumno
        {
            Carne = columnas[0],
            Nombre = columnas[1]
        });
    }

    _context.Alumnos.AddRange(alumnos);

    await _context.SaveChangesAsync();

    return Ok(new
    {
        Registros = alumnos.Count
    });
}
```

> **Nota:** La estrategia de *Batching* mejora significativamente el rendimiento en cargas masivas.

---

### Tecnologías utilizadas

- C#
- ASP.NET Core
- HttpClient
- System.Text.Json
- Entity Framework Core

---

## Resultado esperado

✅ Consumo correcto de una API REST.

✅ Deserialización de objetos JSON.

✅ Lectura eficiente de archivos CSV.

✅ Inserción masiva mediante `AddRange()` y `SaveChangesAsync()`.

---

# Referencias

Facultad de Ingeniería, USAC. (2026). *Sesión 20: Integración de Datos. Consumo de APIs Externas y Carga Masiva (CSV/XML).* Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.
