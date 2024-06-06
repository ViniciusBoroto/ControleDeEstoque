<template>
  <v-dialog>
    <v-container>
      <v-card>
        <v-card-title>{{
          isEditMode ? "Edit Product" : "Create Product"
        }}</v-card-title>
        <v-card-text>
          <v-form @submit.prevent="handleSubmit">
            <v-text-field
              v-model="formData.nome"
              label="Nome"
              required
            ></v-text-field>
            <v-text-field
              v-model="formData.descricao"
              label="Descricao"
              required
            ></v-text-field>
            <v-text-field
              v-model="formData.estoque"
              label="Estoque"
              type="number"
              required
            ></v-text-field>
            <v-select
              v-model="formData.categoria"
              :items="categorias"
              item-text="nome"
              item-value="id"
              label="Categoria"
              required
              :loading="loadingcategorias"
            ></v-select>
            <v-btn type="submit" color="primary">{{
              isEditMode ? "Update" : "Create"
            }}</v-btn>
          </v-form>
          <v-alert
            v-if="successMessage"
            type="success"
            dismissible
            @click:close="successMessage = ''"
          >
            {{ successMessage }}
          </v-alert>
        </v-card-text>
      </v-card>
    </v-container>
  </v-dialog>
</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

// Reactive data
const { InitialCategory } = defineProps(["categoria"]);
const formData = ref({
  nome: "",
  descricao: "",
  estoque: 0,
  categoria: InitialCategory,
});
const categorias = ref([]);
const loadingcategorias = ref(false);
const successMessage = ref("");
const isEditMode = ref(false);

// Router instance
const router = useRouter();

// Fetch categorias from API
const fetchCategorias = async () => {
  loadingcategorias.value = true;
  try {
    const response = await axios.get("https://localhost:7118/api/Categorias");
    categorias.value = response.data;
  } catch (error) {
    console.error("Erro ao obter as categorias:", error);
  } finally {
    loadingcategorias.value = false;
  }
};

// Handle form submission
const handleSubmit = async () => {
  try {
    if (isEditMode.value) {
      // Update product
      await axios.put(
        `https://api.example.com/products/${formData.value.id}`,
        formData.value
      );
      successMessage.value = "Product updated successfully!";
    } else {
      // Create product
      await axios.post("https://api.example.com/products", formData.value);
      successMessage.value = "Product created successfully!";
      // Reset form data
      formData.value = {
        nome: "",
        descricao: "",
        estoque: 0,
        categoria: InitialCategory,
      };
    }
  } catch (error) {
    console.error("Error submitting form:", error);
  }
};

// Fetch categorias on mount
onMounted(fetchCategorias);

// Watch for route params to determine edit mode and load product data if editing
// watch(
//   () => router.currentRoute.value.params.id,
//   async (newId) => {
//     if (newId) {
//       isEditMode.value = true;
//       try {
//         const response = await axios.get(
//           `https://api.example.com/products/${newId}`
//         );
//         formData.value = response.data;
//       } catch (error) {
//         console.error("Error fetching product data:", error);
//       }
//     } else {
//       isEditMode.value = false;
//       formData.value = {
//         name: "",
//         description: "",
//         inventory: 0,
//         category: "",
//       };
//     }
//   },
//   { immediate: true }
// );
</script>

<style scoped>
/* Add custom styles if needed */
</style>
