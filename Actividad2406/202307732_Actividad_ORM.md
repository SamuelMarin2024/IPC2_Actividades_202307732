# Actividad Corta de Laboratorio: De ADO.NET Tradicional a la Automatización con EF Core

**Estudiante:Samuel Ernesto Marin Higueros; Carnet:202307732
**Curso: IPC 2 | Unidad 3 | Ingeniería USAC  

---

## Parte 1: Diagnóstico Técnico y Brecha de Impedancia

### 1. La Brecha de Impedancia
La diferencia conceptual radica en que los lenguajes orientados a objetos (como C#) estructuran su información mediante el uso de referencias, herencia y encapsulamiento; por el contrario, el dominio relacional (SQL) organiza y restringe los datos a través de tuplas, llaves primarias y restricciones[cite: 1]. 

A través de un mapeador como Entity Framework Core, se establecen las siguientes equivalencias exactas entre ambos mundos[cite: 1]:
* **Clase Clásica (POCO)** -> Mapea a -> Tabla (Table)[cite: 1]
* **Propiedad/Atributo** -> Mapea a -> Columna (Column)[cite: 1]
* **Instancia de Objeto** -> Mapea a -> Registro/Fila (Row)[cite: 1]

### 2. Mitigación de Vulnerabilidades
Entity Framework Core previene los fallos de seguridad por inyección SQL de forma completamente nativa[cite: 1]. El framework procesa las solicitudes a través de su rastreador de cambios y autogenera comandos parametrizados de manera interna y transparente para el desarrollador[cite: 1]. 

En la tecnología de ADO.NET tradicional, este riesgo crítico se mitigaba manualmente implementando **Consultas Parametrizadas**[cite: 1]. Para ello, se utilizaban los comandos y colecciones de propiedades de la clase `SqlCommand`, específicamente mediante las instrucciones `cmd.Parameters.Add()` o `cmd.Parameters.AddWithValue()`, las cuales fuerzan al motor de la base de datos a tratar las entradas de los usuarios estrictamente como valores literales invariables en lugar de código ejecutable[cite: 1].

### 3. Optimización de Infraestructura
El uso del método `.AsNoTracking()` en las consultas LINQ desactiva por completo el rastreador de cambios (*Change Tracker*) de Entity Framework Core[cite: 1]. Cuando un flujo requiere exclusivamente la lectura de información para su renderizado en una vista, el seguimiento de modificaciones en memoria RAM se vuelve una sobrecarga innecesaria[cite: 1]. 

Apagar este componente libera memoria RAM crítica en el backend[cite: 1]. En el contexto de la universidad, esto constituye una muestra de **solidaridad computacional** debido a que la optimización algorítmica reduce el uso de CPU y memoria en la infraestructura de hardware compartida de la institución, garantizando que el servicio permanezca disponible, rápido y estable para el resto de los estudiantes del sistema[cite: 1].

---

## Parte 2: Desafío de Refactorización de Código

### 1. El Contexto (`DbContext`)
```csharp
using Microsoft.EntityFrameworkCore;

namespace SistemaControlNotas.Data
{
    public class UnidadAcademicaContext : DbContext
    {
        public UnidadAcademicaContext(DbContextOptions<UnidadAcademicaContext> options) 
            : base(options)
        {
        }

        // DbSet que mapea la entidad Catedratico a la tabla correspondiente
        public DbSet<Catedratico> Catedraticos { get; set; } = null!;
    }
}

---

## Parte 2: Desafío de Refactorización de Código  

###La Consulta LINQ (Versión Refactorizada)

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SistemaControlNotas.Models;

namespace SistemaControlNotas.Services
{
    public class CatedraticoService
    {
        private readonly UnidadAcademicaContext _context;

        public CatedraticoService(UnidadAcademicaContext context)
        {
            _context = context;
        }

        public List<Catedratico> ObtenerCatedraticosModernos()
        {
            // Se utiliza AsNoTracking() para cumplir con el requisito obligatorio de rendimiento en lectura
            return _context.Catedraticos
                .AsNoTracking()
                .Where(c => c.Nombre.StartsWith("Ing."))
                .ToList();
        }
    }
}

---

Parte 3: Referencias Bibliográficas (Obligatorio)

-Facultad de Ingeniería, USAC. (2026). Sesión 17: Conectividad con SQL Server. Acceso 
Estructurado a Datos mediante C# y ADO.NET. Laboratorio de Introducción a la 
Programación y Computación 2. Guatemala. 
-Facultad de Ingeniería, USAC. (2026). Sesión 18: Mapeo de Objetos Relacionales. 
Persistencia Automatizada con Entity Framework Core. Laboratorio de Introducción a la 
Programación y Computación 2. Guatemala.