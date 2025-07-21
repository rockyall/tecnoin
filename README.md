# Tecnoin - Pruebas Técnicas .NET & Web

Este repositorio contiene tres proyectos desarrollados como parte de una evaluación técnica. Cada uno demuestra el uso de tecnologías específicas del stack Microsoft y del desarrollo web moderno, abordando distintos escenarios:

---

## 📌 1. Prueba Técnica – ASP.NET Web + ADO.NET

Una aplicación web desarrollada en ASP.NET MVC que permite gestionar registros de clientes desde una base de datos relacional mediante ADO.NET.

### Funcionalidades:
- Mostrar una tabla HTML con los registros de la tabla `Clientes` (campos: Id, Nombre, Email).
- Insertar nuevos clientes mediante un formulario validado.
- Eliminar clientes por su Id.
- Mostrar mensajes de éxito o error.

### ✅ Requisitos Técnicos Cumplidos:
- [x] Conexión por ADO.NET o ODBC.
- [x] Separación de lógica de datos en clase independiente.
- [x] Validaciones básicas antes de insertar.
- [x] Uso de parameterized queries (contra SQL Injection).
- [x] Manejo de mensajes para el usuario.
- [x] Creación de tabla `Clientes`.

### 🖼️ Captura de pantalla
<img src="prueba1Tabla.png" alt="Vista Clientes" width="600" />
<img src="prueba1Crear.png" alt="Vista Clientes" width="600" />
<img src="prueba1Editar.png" alt="Vista Clientes" width="600" />
<img src="prueba1Borrar.png" alt="Vista Clientes" width="600" />
<img src="prueba1Borrado.png" alt="Vista Clientes" width="600" />

---

## 📌 2. Prueba Técnica – API REST en C# con CRUD

Una API RESTful construida con ASP.NET Core para gestionar productos a través de un conjunto completo de operaciones CRUD.

### 🧪 Endpoints implementados:
- `GET /api/productos` → Lista todos los productos.
- `GET /api/productos/{id}` → Devuelve un producto por su Id.
- `POST /api/productos` → Agrega un nuevo producto.
- `PUT /api/productos/{id}` → Edita un producto existente.
- `DELETE /api/productos/{id}` → Elimina un producto.

### 💻 Modelo de datos:
```csharp
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public double Precio { get; set; }
    public int CantidadEnStock { get; set; }
}
```

---

## 📌 3. Prueba Técnica – Aplicación VueJS integrada en Razor Page

Aplicación frontend desarrollada en VueJS, integrada dentro de una Razor Page para consumir la API de productos y realizar operaciones visuales CRUD.

### Funcionalidades:
- Listado de productos en una tabla HTML.
- Agregar, editar y eliminar productos.
- Formulario con validaciones:
  - El nombre no debe estar vacío.
  - El precio debe ser mayor que cero.
- Botones de acción dentro de la tabla para editar o eliminar productos.

### 🧩 Estructura principal (Vue embebido en Razor)
```html
<!-- Productos.cshtml -->
<div id="app">
  <table>
    <tr v-for="producto in productos" :key="producto.id">
      <td>{{ producto.nombre }}</td>
      <td>{{ producto.precio }}</td>
      <td>
        <button @click="editarProducto(producto)">Editar</button>
        <button @click="eliminarProducto(producto.id)">Eliminar</button>
      </td>
    </tr>
  </table>

  <form @submit.prevent="guardarProducto">
    <input v-model="form.nombre" placeholder="Nombre" required />
    <input v-model.number="form.precio" type="number" min="0.01" required />
    <button type="submit">Guardar</button>
  </form>
</div>

<script src="https://cdn.jsdelivr.net/npm/vue@2"></script>
<script>
  new Vue({
    el: '#app',
    data: {
      productos: [],
      form: {
        id: null,
        nombre: '',
        precio: 0
      }
    },
    methods: {
      async obtenerProductos() {
        const res = await fetch('/api/productos');
        this.productos = await res.json();
      },
      async guardarProducto() {
        // POST o PUT según sea nuevo o edición
      },
      async eliminarProducto(id) {
        await fetch(`/api/productos/${id}`, { method: 'DELETE' });
        this.obtenerProductos();
      },
      editarProducto(prod) {
        this.form = Object.assign({}, prod);
      }
    },
    mounted() {
      this.obtenerProductos();
    }
  });
</script>
```

---

## 🛠️ Cómo ejecutar cada proyecto

Asegúrate de tener instalados **.NET SDK** y **Node.js** (solo si usas herramientas externas para compilar VueJS, aunque en este caso se usó Vue desde CDN).

### 1. ASP.NET Web (Clientes)
```bash
cd ClientesApp
dotnet build
dotnet run
```

### 2. API REST (Productos)
```bash
cd ProductosApi
dotnet build
dotnet run
```

### 3. Frontend VueJS (integrado Razor)
Este frontend está integrado directamente en la vista Razor de ASP.NET. Al ejecutar el proyecto de Razor Pages, ya podrás ver la interfaz Vue funcionando:

```bash
cd ProductosFrontendRazor
dotnet run
```

Luego abre el navegador en:  
📍 `http://localhost:5000/Productos`

---

## 📷 Capturas de pantalla

<img src="imagenes/api-response.png" width="600" />
<img src="imagenes/vue-ui.png" width="600" />

---

## 🔗 Requisitos

- .NET Core SDK  
- Visual Studio o VS Code  
- MySQL o SQL Server  
- Node.js (opcional para herramientas de build JS)

---

## ✍ Autor

**Ricardo Ochoa Hernández**  
[@rockyall](https://github.com/rockyall)
