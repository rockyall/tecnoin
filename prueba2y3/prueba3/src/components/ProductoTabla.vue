<template>
  <div class="container-fluid w-100 ">
    <div class="row w-100 vh-100 align-items-center justify-content-center">
        <div class="col-10">
            <h1>Gestión de Productos</h1>

            <!-- Create / Edit Form -->
            <form @submit.prevent="saveProduct" class="form" >
            <input class="form-control" v-model="form.nombre" placeholder="Nombre" required />
            <input class="form-control" v-model="form.descripcion" placeholder="Descripción" required />
            <input class="form-control" type="number" v-model.number="form.precio" placeholder="Precio" required min="0.00" />
            <input class="form-control" type="number" v-model.number="form.cantidadEnStock" placeholder="Cantidad en Stock" required min="0.0" />
            <button class="btn btn-outline-secondary" type="submit">{{ form.id ? "Actualizar" : "Crear" }}</button>
            <button class="btn btn-outline-secondary" type="button" @click="resetForm">Cancelar</button>
            </form>

            <!-- Table View -->
            <table class="table table-sm table-borderless">
            <thead>
                <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Descripción</th>
                <th>Precio</th>
                <th>Cantidad</th>
                <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="p in productos" :key="p.id">
                <td>{{ p.id }}</td>
                <td>{{ p.nombre }}</td>
                <td>{{ p.descripcion }}</td>
                <td>{{ p.precio }}</td>
                <td>{{ p.cantidadEnStock }}</td>
                <td>
                    <div class="d-flex align-items-center">
                        <button class="btn" @click="editProduct(p)">Editar</button>
                        <button class="btn ms-2" @click="deleteProduct(p.id)">Eliminar</button>    
                    </div>
                </td>
                </tr>
            </tbody>
            </table>
        </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const productos = ref([])
const form = ref({
  id: null,
  nombre: '',
  descripcion: '',
  precio: 0,
  cantidadEnStock: 0
})

const apiBase = '/api/productos'

const loadProductos = async () => {
  const res = await fetch(apiBase)
  productos.value = await res.json()
}

const saveProduct = async () => {
  if (form.value.precio <= 0) {
    alert("El precio debe ser mayor que 0");
    return;
  }

  const method = form.value.id ? 'PUT' : 'POST'
  const url = form.value.id ? `${apiBase}/${form.value.id}` : apiBase

  const res = await fetch(url, {
    method,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(form.value)
  })

  if (res.ok) {
    await loadProductos()
    resetForm()
  } else {
    alert("Error al guardar el producto")
  }
}

const deleteProduct = async (id) => {
  if (!confirm("¿Eliminar este producto?")) return
  const res = await fetch(`${apiBase}/${id}`, { method: 'DELETE' })
  if (res.ok) loadProductos()
  else alert("Error al eliminar el producto")
}

const editProduct = (producto) => {
  form.value = { ...producto }
}

const resetForm = () => {
  form.value = {
    id: null,
    nombre: '',
    descripcion: '',
    precio: 0,
    cantidadEnStock: 0
  }
}

onMounted(loadProductos)
</script>

<style scoped>
.container {
  max-width: 800px;
  margin: auto;
}
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}
th, td {
  padding: 0.5rem;
  border: 1px solid #ddd;
}
form {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
  flex-wrap: wrap;
}
input {
  flex: 1 1 150px;
  padding: 0.4rem;
}
button {
  padding: 0.4rem 0.8rem;
}
</style>
