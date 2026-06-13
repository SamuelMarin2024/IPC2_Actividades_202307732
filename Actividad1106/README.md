#LABORATORIO INTRODUCCION A LA PROGRAMACION Y COMPUTACION 2 Sección P
#De: Samuel Marín
#Carnet: 202307732
# Actividad de Investigación y Práctica: Estructuras de Datos Avanzadas y APIs con ASP.NET Core

## Parte 1: Investigación Teórica

### 1. Estructuras de Datos Eficientes

#### Árboles Binarios de Búsqueda (ABB)
* **Regla de ordenamiento:** Para cualquier nodo del árbol, todos los elementos en su subárbol izquierdo deben tener un valor menor al del nodo, y todos los elementos en su subárbol derecho deben tener un valor mayor.
* **Principal desventaja (Degeneración):** Cuando los datos se insertan en un orden estrictamente secuencial o pre-ordenado (por ejemplo: 1, 2, 3, 4, 5), el árbol crece de forma completamente lineal hacia un solo lado. Esto provoca que se degenere en una lista vinculada, haciendo que la complejidad de búsqueda pase de un estado óptimo de $O(\log n)$ a una ineficiente búsqueda secuencial de $O(n)$.


#### Árboles AVL
* **Definición de árbol auto-balanceado:** Es un Árbol Binario de Búsqueda que se reorganiza automáticamente (mediante rotaciones de nodos) cuando detecta una diferencia crítica en las alturas de sus subárboles, asegurando que el árbol permanezca lo más compacto y simétrico posible.
* **Factor de Balanceo:** Se calcula para cada nodo utilizando la fórmula:
$$\text{Factor} = \text{Altura}_{\text{Izquierda}} - \text{Altura}_{\text{Derecha}}$$
Para que el árbol se considere balanceado, el factor de balance de cada nodo individual debe pertenecer exclusivamente al conjunto $\{-1, 0, 1\}$. Si pasa a $2$ o $-2$, el nodo requiere una rotación.
* **Complejidad $O(\log n)$:** Debido a que el algoritmo AVL garantiza estrictamente que la altura total del árbol se mantenga proporcional al logaritmo del número de elementos ($n$), el camino máximo desde la raíz hasta cualquier hoja es muy corto. Por ende, las operaciones de búsqueda, inserción y eliminación mantienen siempre un límite superior de complejidad temporal de $O(\log n)$.


---

### 2. Fundamentos de Web APIs

#### ¿Qué es una API y el modelo Cliente-Servidor?
* **API (Application Programming Interface):** Es un conjunto de reglas y definiciones que permite que diferentes aplicaciones de software se comuniquen e intercambien datos entre sí de forma estandarizada.
* **Modelo Cliente-Servidor:** Es un modelo de diseño de software distribuido donde las tareas se reparten entre los proveedores de recursos o servicios (Servidores) y los demandantes de dichos servicios (Clientes, como navegadores web o aplicaciones móviles).

#### Ciclo de Petición (Request) y Respuesta (Response) en HTTP
1. **Request (Petición):** El cliente emite un mensaje estructurado a través de la red que incluye un método/verbo HTTP (qué quiere hacer), una URL/URI (a qué recurso se dirige), cabeceras (headers) con metadatos como el tipo de contenido, y opcionalmente un cuerpo (body) con datos.
2. **Procesamiento:** El servidor web recibe e interpreta la petición, ejecuta la lógica de negocio necesaria (o consultas a bases de datos) y prepara un resultado.
3. **Response (Respuesta):** El servidor devuelve un mensaje compuesto por un código de estado HTTP (que indica el éxito o error de la transacción), cabeceras de respuesta y un cuerpo que habitualmente contiene datos estructurados en formato JSON o HTML.


#### Verbos HTTP: GET vs POST

| Verbo HTTP | Propósito Conceptual | Uso Correcto | Idempotencia |
| :--- | :--- | :--- | :--- |
| **GET** | Recuperación de recursos. | Solicitar o leer representaciones de datos existentes sin modificar el estado del servidor. | **Sí es idempotente:** Ejecutar la misma petición GET 100 veces seguidas producirá el mismo resultado y no alterará los datos del sistema. |
| **POST** | Creación de nuevos recursos. | Enviar datos en el cuerpo de la petición para que el servidor cree una nueva entidad. | **No es idempotente:** Si envías la misma petición POST 5 veces, el servidor creará 5 registros o nodos diferentes en la base de datos de manera duplicada. |