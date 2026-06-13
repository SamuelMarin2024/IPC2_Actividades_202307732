var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor (necesario para Swagger si está activo)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline HTTP para desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. Configuración del almacenamiento en memoria (Lista estática)
var coleccionNodos = new List<NodoElemento>
{
    new NodoElemento { Id = 10, Valor = "Raíz Inicial (ABB)" },
    new NodoElemento { Id = 5, Valor = "Hijo Izquierdo" }
};

// 4. Implementación de Endpoints

// A. EJEMPLO DE GET: Retorna todos los nodos actuales
app.MapGet("/api/nodos", () => Results.Ok(coleccionNodos));

// B. EJEMPLO DE POST: Recibe un nuevo nodo y lo "inserta" en la colección
app.MapPost("/api/nodos", (NodoElemento nuevoNodo) =>
{
    // Validación simple
    if (nuevoNodo.Id <= 0 || string.IsNullOrEmpty(nuevoNodo.Valor))
    {
        return Results.BadRequest("Datos del nodo inválidos.");
    }

    // Agregar el nodo enviado a la lista en memoria
    coleccionNodos.Add(nuevoNodo);

    // Retorna un código de estado 201 Created y la URI del nuevo recurso
    return Results.Created($"/api/nodos/{nuevoNodo.Id}", nuevoNodo);
});

app.Run();

// 2. Modelado del Recurso (El "Nodo") colocado al final del archivo o fuera del flujo principal
public class NodoElemento
{
    public int Id { get; set; }
    public string Valor { get; set; } = string.Empty;
}
