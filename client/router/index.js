import { createRouter, createWebHistory } from 'vue-router'
import ProductEdit from '@/components/ProductEdit.vue'
import Home from '@/components/Home.vue'

const routes = [
  {
    path: '/categorias/editar/:id',
    name: 'EditarCategoria',
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
