... (Aquí dejas toda la Parte 1 de la teoría que te pasé en el primer mensaje) ...

---

## Parte 3: Verificación y Pruebas de la API

A continuación se adjuntan las evidencias del correcto funcionamiento de los endpoints implementados en ASP.NET Core utilizando la interfaz de Swagger.

### 1. Evidencia de Petición GET (Obtener Nodos)
Se realizó la petición al endpoint `/api/nodos` obteniendo de manera exitosa los datos iniciales cargados en la memoria estática con un código de estado `200 OK`.

![Prueba Endpoint GET](images/captura_get.png)

### 2. Evidencia de Petición POST (Insertar Nodo)
Se envió un nuevo objeto JSON simulando la inserción de un elemento derecho que balancearía un árbol AVL. El servidor procesó la información correctamente respondiendo con un código de estado `201 Created`.

![Prueba Endpoint POST](images/captura_post.png)