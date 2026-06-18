#LABORATORIO INTRODUCCION A LA PROGRAMACION Y COMPUTACION 2 Sección P
#De: Samuel Marín
#Carnet: 202307732
---
# Actividad de Laboratorio: Arquitectura Multi-Nivel (N-Tier) y Patrón MVC en .NET

## Parte 1: Fundamentación Teórica y Análisis Crítico

### 1. El Tránsito hacia los Sistemas Distribuidos y Multi-Capa

#### La Limitación del Monolito Local

Cuando la interfaz de usuario, la lógica de negocio y el almacenamiento de datos se encuentran en una sola máquina física, el sistema presenta problemas de escalabilidad y sincronización. Todos los usuarios dependen de un único equipo, lo que limita el crecimiento de la aplicación. Además, si la máquina falla, todo el sistema queda fuera de servicio. Este enfoque también dificulta el acceso concurrente y el mantenimiento de la información.

#### Distinción Crítica: Layers vs. Tiers

Las **capas (Layers)** representan una separación lógica dentro del código fuente de la aplicación. Su propósito es organizar responsabilidades y facilitar el mantenimiento.

Los **niveles (Tiers)** representan una separación física de los componentes en diferentes servidores o equipos. Cada nivel puede ejecutarse en hardware distinto y comunicarse a través de una red.

En resumen:

* **Layers:** organización lógica del software.
* **Tiers:** distribución física de la infraestructura.

#### Responsabilidades en la Arquitectura de 3 Niveles

##### Nivel 1: Capa de Presentación (Presentation Tier)

Su función es interactuar con el usuario, mostrar información y capturar datos de entrada. Generalmente utiliza tecnologías como HTML, CSS, JavaScript, Razor Views o navegadores web.

##### Nivel 2: Capa de Aplicación o Negocio (Application Tier)

Contiene las reglas de negocio, validaciones y procesamiento de información. Actúa como intermediario entre la presentación y los datos. En .NET suele implementarse mediante controladores, servicios y clases de negocio.

##### Nivel 3: Capa de Datos (Data Tier)

Es responsable del almacenamiento, recuperación y administración de la información. Generalmente utiliza sistemas gestores de bases de datos como SQL Server, MySQL o PostgreSQL.

#### Seguridad Perimetral

Exponer directamente una base de datos a Internet representa un riesgo crítico porque un atacante podría intentar acceder directamente a la información almacenada, ejecutar consultas maliciosas o comprometer la integridad de los datos.

La buena práctica consiste en mantener la base de datos en una red privada y permitir el acceso únicamente desde la capa de aplicación. De esta forma, el servidor de aplicaciones actúa como una barrera de seguridad entre los usuarios y la base de datos.

---

### 2. Desacoplamiento Lógico con el Patrón MVC

#### La Crisis del Código Espagueti

Cuando las consultas SQL, la lógica de negocio y la interfaz gráfica se mezclan en un mismo archivo, el código se vuelve difícil de mantener, depurar y ampliar. Además, las pruebas unitarias se complican porque las responsabilidades no están claramente separadas.

Este problema genera un alto acoplamiento y una baja mantenibilidad del software.

#### Separación de Preocupaciones (SoC)

##### Modelo (Model)

Representa los datos y las entidades del sistema. Su responsabilidad es almacenar y gestionar la información. El modelo no debe conocer cómo se presentan los datos al usuario.

##### Vista (View)

Es la encargada de mostrar la información al usuario. Se considera una entidad pasiva porque recibe datos y los presenta visualmente. No debe contener lógica de negocio ni consultas directas a bases de datos.

##### Controlador (Controller)

Actúa como intermediario entre la vista y el modelo. Recibe las solicitudes del usuario, coordina el procesamiento de la información y decide qué vista debe mostrarse.

#### Métricas de Ingeniería de Software

El patrón MVC promueve una **alta cohesión** porque cada componente posee una responsabilidad específica y bien definida.

También favorece un **bajo acoplamiento**, ya que los componentes interactúan mediante interfaces claras y controladas, reduciendo las dependencias entre ellos.

Como resultado, las aplicaciones son más fáciles de mantener, probar, extender y reutilizar.

---

## Parte 2: Modelado del Ciclo de Vida y Enrutamiento Semántico

### 1. Mapeo Analítico de URLs

| URL Entrante                                                 | Clase Controladora         | Método (Acción) | Parámetro id       |
| ------------------------------------------------------------ | -------------------------- | --------------- | ------------------ |
| https://ingenieria.usac.edu.gt/ControlAcademico/Login        | ControlAcademicoController | Login           | Ninguno            |
| https://ingenieria.usac.edu.gt/Estudiante/Historial/20260123 | EstudianteController       | Historial       | 20260123           |
| https://ingenieria.usac.edu.gt/Asignacion/Detalle/10         | AsignacionController       | Detalle         | 10                 |
| https://ingenieria.usac.edu.gt/Home                          | HomeController             | Index           | Ninguno (Opcional) |

### 2. Diagramación del Flujo Interactivo

#### Paso 1

El usuario realiza una acción desde su navegador web, como hacer clic en un enlace o enviar un formulario. Esto genera una solicitud HTTP hacia el servidor.

#### Paso 2

El sistema de enrutamiento de ASP.NET Core analiza la URL recibida y determina qué controlador y acción deben ejecutarse.

#### Paso 3

El controlador recibe la petición, valida los datos necesarios y solicita la información correspondiente al modelo.

#### Paso 4

El modelo procesa o recupera los datos requeridos y los devuelve al controlador.

#### Paso 5

El controlador envía los datos a la vista, la cual genera el HTML dinámicamente. Finalmente, el servidor devuelve la respuesta al navegador para que el usuario visualice la página.

---

## Parte 4: Auditoría y Control de Calidad

### 1. Prueba de Cohesión (GET)

La acción `Listar()` mantiene una única responsabilidad: obtener la información de los estudiantes almacenados en memoria y enviarla a la vista. No realiza cálculos complejos ni contiene consultas SQL, por lo que cumple el principio de alta cohesión.

### 2. Evaluación de Antipatrones

Los métodos implementados en `EstudianteController` son cortos y específicos. Ninguno supera las 20 líneas de código, evitando el antipatrón conocido como **Fat Controller** y favoreciendo el mantenimiento del sistema.

---

## Parte 5: Referencias Bibliográficas

* Facultad de Ingeniería, USAC. (2026). *Sesión 11: Modelado Base y Arquitecturas de Despliegue. Evolución de Sistemas Distribuidos, Fundamentos del Modelo Cliente-Servidor y Diseño Físico Multi-Capas (N-Tier).* Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.

* Facultad de Ingeniería, USAC. (2026). *Sesión 12: Arquitectura y Componentes del Patrón MVC. Desacoplamiento Lógico de Software, Ciclo de Vida de las Peticiones y Enrutamiento en Aplicaciones Interactivas Modernas.* Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.
