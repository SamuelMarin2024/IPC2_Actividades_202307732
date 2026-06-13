#LABORATORIO INTRODUCCION A LA PROGRAMACION Y COMPUTACION 2 Sección P
#De: Samuel Marín
#Carnet: 202307732
# Actividad de Investigación y Práctica: Balanceo Compuesto en Árboles AVL y APIs Web

**Estudiante:** Samuel Marín  
**Entorno de Desarrollo:** Microsoft Visual Studio / ASP.NET Core (Minimal APIs)

---

## Parte 1: Investigación Teórica y Análisis de Casos

### 1. El Límite de las Rotaciones Simples y Desbalanceo en "Zig-Zag"

#### El Problema Cruzado
Las rotaciones simples (como la rotación simple a la izquierda o a la derecha) están diseñadas para resolver desbalances puramente lineales ("Zig-Zig"), donde la inserción ocurre enteramente en el extremo exterior de un subárbol crítico. Cuando se introduce una secuencia cruzada o en "Zig-Zag" como **30, 10, 20**, el nodo raíz (30) se desbalancea hacia la izquierda, pero su hijo izquierdo (10) tiene una carga hacia la derecha debido a la inserción del 20. 

Si intentáramos aplicar una rotación simple a la derecha sobre el nodo 30, el nodo 10 pasaría a ser la raíz y el 30 su hijo derecho. Sin embargo, el nodo 20 (que era hijo derecho de 10) pasaría a ser el hijo izquierdo de 30. El árbol resultante seguiría estando desbalanceado, simplemente transfiriendo la inclinación crítica del lado izquierdo al lado derecho. Las rotaciones simples fallan porque cambian la orientación del problema sin resolver la asimetría de las alturas en los niveles inferiores.

#### Definición Matemática del Factor de Equilibrio (FE)
El factor de equilibrio de un nodo se define convencionalmente bajo la métrica:

$$FE = \text{Altura}(\text{Subárbol Derecho}) - \text{Altura}(\text{Subárbol Izquierdo})$$

Bajo esta definición, una **Rotación Doble Izquierda-Derecha (RID)** se gatilla obligatoriamente cuando se cumplen las siguientes condiciones de desbalance en una relación abuelo-padre:

* **Nodo Padre (Abuelo crítico):** $FE(\text{Padre}) = -2$ (indicando una sobrecarga en su subárbol izquierdo).
* **Nodo Hijo (Hijo Izquierdo):** $FE(\text{Hijo\_Izquierdo}) = +1$ (indicando que su altura predomina en su propio subárbol derecho, formando el patrón cruzado).

#### Principio DRY (Don't Repeat Yourself)
La implementación de operaciones compuestas utilizando las primitivas de rotación simple ofrece ventajas cruciales en la ingeniería de software:
* **Reducción de Complejidad y Errores:** Evita la reasignación manual de punteros o referencias desde cero, una operación matemática e informática propensa a generar referencias circulares, pérdidas de nodos (*memory leaks*) o desconfiguraciones de alturas.
* **Mantenibilidad y Reutilización:** Una rotación doble RID se puede descomponer conceptualmente en dos instrucciones atómicas ya probadas: una rotación simple a la izquierda sobre el hijo izquierdo, seguida de una rotación simple a la derecha sobre el nodo raíz desbalanceado. Reutilizar código validado disminuye drásticamente los puntos de falla en el sistema de datos.

### 2. Fundamentos de Arquitectura Web y Protocolo HTTP

#### El Modelo Cliente-Servidor en la Web
Cuando un cliente web (por ejemplo, una aplicación frontend o una herramienta de pruebas como Postman) solicita un recurso, interactúa con el servidor mediante el ciclo de **Petición-Respuesta (Request-Response)** a través del protocolo HTTP sobre TCP/IP:
* **Petición (Request):** El cliente empaqueta la solicitud incluyendo una línea de estado (Método HTTP como GET/POST y la URI), un conjunto de encabezados (*Headers* con metadatos como el tipo de contenido o credenciales) y un cuerpo (*Body*) opcional en formatos como JSON que transporta la información que se desea procesar.
* **Respuesta (Response):** El servidor procesa el requerimiento contra sus estructuras en memoria o bases de datos y devuelve un paquete con un código de estado HTTP (ej. 200 OK, 201 Created), encabezados de respuesta y un cuerpo que contiene la representación del recurso solicitado (la estructura del árbol en formato estructurado).

#### Semántica de Operaciones: GET vs POST
* **HTTP GET:** Diseñado estrictamente para la **recuperación y lectura** de recursos. Es una operación segura e idempotente; no altera el estado del servidor ni modifica los datos subyacentes. En el contexto de esta actividad, se utiliza para consultar la topología o el estado de los nodos del árbol en memoria sin alterarlos.
* **HTTP POST:** Diseñado para la **mutación, creación o inserción** de nuevos elementos en el servidor. No es seguro ni idempotente, ya que múltiples ejecuciones idénticas crearán múltiples recursos o alterarán el estado del sistema repetidas veces. En nuestra simulación, se utiliza para enviar un nuevo nodo en el cuerpo de la petición y disparar el algoritmo de inserción y balanceo dinámico.

---

## Parte 2: Fragmentos de Código de la API Funcional

### Modelo del Nodo (`NodoAVL.cs`)
```csharp
public class NodoAVL
{
    public int Id { get; set; } 
    public string Etiqueta { get; set; } = string.Empty;
    public int Altura { get; set; } = 1;
}